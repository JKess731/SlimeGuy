using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementFollowStop : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float distanceBetween;
    public float stop;

    private float distance;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame. On update checks the distabce between the player and this enemy and the direction to the player.
    //Then normalizes the direction and uses a float angle for better enemy turning and movement. Then the enemy checks if the distance to
    //the player is less then the given distanceBetween (detection range) and greater then the given stop. If it is the enemy will follow the
    //player until ethier the player is out of detetcion range or stop point is hit. Used for ranged enemies.A
    void Update(){
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
        if (distance < distanceBetween) {
            if (distance > stop) {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }  
        }
    }
}
