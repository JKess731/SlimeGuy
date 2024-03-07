using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolemBehavior : MonoBehaviour
{
    //Enemy script.
    [SerializeField] private float speed;
    [SerializeField] public int dangerLevel;

    //EnemyMovementFollow script.
    [SerializeField] GameObject enemy;
    private Transform player;
    public float distanceBetween;
    private float distance;
    private bool onPlayer = false;
    private bool foundPlayer = false;
    public float stop;

    //EnemyAttackSlam script
    [SerializeField] private int slamDamage;
    [SerializeField] private int volleyDamage;
    [SerializeField] private float slamAttackDelay;
    public PlayerMove playerMove;
    private bool attacking = false;
    public GameObject ring;

    //RockVolley
    [SerializeField] private float rockVolleyAttackDelay;
    public GameObject volleyAttackCircle;
    private float shotCooldown;
    public float startShotCooldown;
    private Vector3 lastPosition = new Vector3();
    public float detectRange;


    //Attack cycles
    private float attackCounter = 0;
    private bool attackOnce = true;


    private void Awake()
    {
        player = GameObject.FindWithTag("player").transform;
        playerMove = GameObject.FindWithTag("player").GetComponent<PlayerMove>();
        shotCooldown = startShotCooldown;
        StartCoroutine(AttackCycleSwitch());
    }


    void Update()
    {

        //From EnemyMovementFollow script.
        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
        if (attackCounter > .5)
        {
            if (distance < distanceBetween)
            {
                if (distance > stop)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }
            }

            //RockVolleyMovementBehavior
            float boomerangDistance = Vector2.Distance(transform.position, player.transform.position);
            Vector3 boomerangRotation = player.transform.position - ring.transform.position;
            float rotZ = Mathf.Atan2(boomerangRotation.y, boomerangRotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            if (distance < detectRange)
            {
                if (transform.position == lastPosition)
                {
                    if (shotCooldown <= 0)
                    {
                        if (attackOnce == true)
                        {
                            attackOnce = false;
                            StartCoroutine(RockVolleyAttack());
                        }
                    }
                    else
                    {
                        shotCooldown -= Time.deltaTime;
                    }
                }
                else
                {
                    shotCooldown = startShotCooldown;
                }
                lastPosition = transform.position;
            }
            lastPosition = transform.position;
        }
        else
        {
            if (onPlayer == false)
            {
                if (foundPlayer == false)
                {
                    if (distance < distanceBetween)
                    {
                        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                        foundPlayer = true;
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }
            }

            //From EnemyAttackSlam script.
            if (attacking == false)
            {
                Vector3 rotation = player.transform.position - ring.transform.position;
                float RotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                ring.transform.rotation = Quaternion.Euler(0, 0, RotZ);
            }

        }
    }

    //---------------------------------------------------------------------------------------------------------------------------
    //THIS SECTION HANDLES FUNCTIONS FROM EnemyMovementFollow script.
    public void AttackColliding()
    {
        onPlayer = true;
    }

    public void AttackNotColliding()
    {
        onPlayer = false;
    }


    //---------------------------------------------------------------------------------------------------------------------------
    //THIS SECTION HANDLES FUNCTIONS FROM EnemyAttackSlam script.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackCounter > .5)
        {
            
        }
        else 
        {
            if (collision.gameObject.name == "Player")
            {
                AttackColliding();
                StartCoroutine(SlamAttackingContinue());
            }
        }
    }

    IEnumerator SlamAttackingContinue()
    {
        attacking = true;
        yield return new WaitForSeconds(slamAttackDelay / 2);
        ring.GameObject().GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(slamAttackDelay / 2);
        ring.GameObject().GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponentInParent<PlayerHealth>().Damage(slamDamage);
        StartCoroutine(PlayerStunned());
        attacking = false;
        RestartAttackCounter();
        AttackNotColliding();



    }

    IEnumerator PlayerStunned()
    {
        playerMove.DisableMovemnt();
        Debug.Log("Player Stunned");
        yield return new WaitForSeconds(slamAttackDelay / 2);
        playerMove.EnableMovemnt();
    }


    //---------------------------------------------------------------------------------------------------------------------------
    //THIS HANDLES COROUTINE FOR ROCKVOLLEY.
    IEnumerator RockVolleyAttack()
    {
        yield return new WaitForSeconds(rockVolleyAttackDelay/2);
        Vector3 playerPos = player.transform.position;
        GameObject volley1 = volleyAttackCircle.gameObject;
        volley1.gameObject.GetComponent<EnemyBulletVolley>().delayVolleyTime = rockVolleyAttackDelay;
        Instantiate(volley1, playerPos, ring.transform.rotation);
        yield return new WaitForSeconds(rockVolleyAttackDelay / 2);
        RestartAttackCounter();
        AttackNotColliding();

    }


    //---------------------------------------------------------------------------------------------------------------------------
    //THIS HANDLES ATTACK CYCLES

    IEnumerator AttackCycleSwitch()
    {
        yield return new WaitForSeconds(.5f);
        attackCounter += Random.value;
        //<= .5 = slash
        //> .5 = Throw
    }

    public void RestartAttackCounter()
    {
        onPlayer = false;
        attackCounter = 0;
        StartCoroutine(AttackCycleSwitch());
        attackOnce = true;
        shotCooldown = startShotCooldown;
    }
}
