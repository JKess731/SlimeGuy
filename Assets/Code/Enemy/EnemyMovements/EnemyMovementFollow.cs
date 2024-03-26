using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFollow: MonoBehaviour
{

    [SerializeField] GameObject enemy;
    private Transform player;
    public float speed;
    public float distanceBetween;
    private float distance;
    private bool onPlayer = false;
    private bool foundPlayer = false;
    private float startSecondAttack = 0;
    private bool continueSecondAttackCounter = true;


    private void Awake()
    {
        player = GameObject.FindWithTag("player").transform;
        startSecondAttack += Random.value;
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
        if (continueSecondAttackCounter == true) {
            continueSecondAttackCounter = false;
            StartCoroutine(StartSecondAttackCounter());
        }
        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
        if (onPlayer == false)
        {
            if (startSecondAttack >= 10)
            {
                onPlayer = true;
                continueSecondAttackCounter = false;
                if (enemy.name == "Dwarf") {
                    enemy.GetComponent<EnemyAttackThrowBommerang>().RestartCooldown();

                }
            }


            if (foundPlayer == false)
            {
                if (distance < distanceBetween)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                    foundPlayer = true;
                }
            }
            else {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
        }
    }

    public void AttackColliding()
    {
        onPlayer = true;
    }

    public void AttackNotColliding()
    {
        onPlayer = false;
    }

    public void RestartSecondAttackCounter() {

        onPlayer = false;
        continueSecondAttackCounter = true;
        startSecondAttack = 0;
    }

    IEnumerator StartSecondAttackCounter() { 
        yield return new WaitForSeconds(.5f);
        startSecondAttack += Random.value * 2;
        Debug.Log(startSecondAttack);
        continueSecondAttackCounter = true;
    }
}
