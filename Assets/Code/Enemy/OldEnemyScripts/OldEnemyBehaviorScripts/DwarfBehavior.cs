using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DwarfBehavior : MonoBehaviour
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


    //EnemyAttackSlash script.
    [SerializeField] private int slashDamage;
    [SerializeField] private float attackDelay;
    private bool attacking = false;
    public GameObject ring;

    //EnemyAttackThrowBoomerang script.
    public GameObject boomerangObject;
    public Transform boomerangPos;
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
        shotCooldown = startShotCooldown;
        StartCoroutine(AttackCycleSwitch());
    }

    // Update is called once per frame
    void Update(){
        //From EnemyMovementFollow script.
        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
        if (attackCounter > .5){
            if (distance < distanceBetween){
                if (distance > stop){
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }
            }

            //From EnemyAttackThrowBoomerang Script
            float boomerangDistance = Vector2.Distance(transform.position, player.transform.position);
            Vector3 boomerangRotation = player.transform.position - ring.transform.position;
            float rotZ = Mathf.Atan2(boomerangRotation.y, boomerangRotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            if (distance < detectRange){
                if (transform.position == lastPosition){
                    if (shotCooldown <= 0){
                        if(attackOnce == true) {
                            attackOnce = false;
                            StartCoroutine(ThrowAttack());
                        }  
                    }
                    else{
                        shotCooldown -= Time.deltaTime;
                    }
                }
                else{
                    shotCooldown = startShotCooldown;
                }
                lastPosition = transform.position;
            }
            lastPosition = transform.position;
        }
        else{
            if (onPlayer == false){
                if (foundPlayer == false){
                    if (distance < distanceBetween){
                        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                        foundPlayer = true;
                    }
                }
                else{
                    transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }
            }

            //From EnemyAttackSlash script.
            if (attacking == false){
                Vector3 rotation = player.transform.position - ring.transform.position;
                float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                ring.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);
            }
        } 
    }

    //---------------------------------------------------------------------------------------------------------------------------
    //THIS SECTION HANDLES FUNCTIONS FROM EnemyMovementFollow script.
    public void AttackColliding(){
        onPlayer = true;
    }

    public void AttackNotColliding(){
        onPlayer = false;
    }

    //---------------------------------------------------------------------------------------------------------------------------
    //THIS SECTION HANDLES SLASH ATTACK COLLISION AND COROUTINE from EnemyAttackSlash script.
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Player"){
            AttackColliding();
            StartCoroutine(SlashAttackingContinue());
        }
    }


    IEnumerator SlashAttackingContinue(){
        attacking = true;
        yield return new WaitForSeconds(attackDelay / 2);
        ring.GameObject().GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(attackDelay / 2);
        ring.GameObject().GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponentInParent<PlayerStats>().Damage(slashDamage);
        attacking = false;
        RestartAttackCounter();
        AttackNotColliding();
    }

    //---------------------------------------------------------------------------------------------------------------------------
    //THIS HANDLES COROUTINE FOR THROWING THE BOOMERANG. FROM EnemyAttackThrowBommerang SCRIPT.
    IEnumerator ThrowAttack(){
        yield return new WaitForSeconds(1f);
        GameObject bullet1 = Instantiate(boomerangObject, boomerangPos.position, ring.transform.rotation);
        bullet1.gameObject.GetComponent<EnemyBulletReturn>().enemy = enemy;
        yield return new WaitForSeconds(1f);
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

    public void RestartAttackCounter() {
        onPlayer = false;
        attackCounter = 0;
        StartCoroutine(AttackCycleSwitch());
        attackOnce = true;
        shotCooldown = startShotCooldown;
    }
}
