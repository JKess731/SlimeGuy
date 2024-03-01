using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFollow: MonoBehaviour
{
    private Transform player;
    public float speed;
    public float distanceBetween;
    private float distance;
    private bool onPlayer = false;


    private void Awake()
    {
        player = GameObject.FindWithTag("player").transform;
    }

    /*
     * On Awake grab reference to the player from the scene
     * 
     *  Jared Kessler
     *  2/14/24
     *  3:45 PM
     * 
     */

    // Update is called once per frame. On update checks the distabce between the player and this enemy and the direction to the player.
    //Then normalizes the direction and uses a float angle for better enemy turning and movement. Then if the distance between the player and
    //this enemy is less then the given distanceBetween (The detection range for this enemy) then the enemy will follow the player. If the 
    //player leaves the detection range then the enemy will stop moving. Used for melee enemies.
    void Update(){
        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
        if (onPlayer == false)
        {
            if (distance < distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }

        }
        
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {

    //        attackColliding();
    //    }

    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        attackNotColliding();

    //    }

    //}

    public void AttackColliding()
    {
        onPlayer = true;
        

    }



    public void AttackNotColliding()
    {
        onPlayer = false;
        Debug.Log("AttakNotCollide");

    }

}
