using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DwarfBehavior : MonoBehaviour
{
    //Enemy script.
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private int defense;
    [SerializeField] public int dangerLevel;

    //EnemyMovementFollow script.
    [SerializeField] GameObject enemy;
    private Transform player;
    public float distanceBetween;
    private float distance;
    private bool onPlayer = false;
    private bool foundPlayer = false;


    //EnemyAttackSlash script.
    [SerializeField] private int slashDamage;
    [SerializeField] private float attackDelay;
    private bool slashAttackCon = false;
    private bool attacking = false;
    public GameObject ring;
    public Collider2D slashCollider;

    //EnemyAttackThrowBoomerang script.
    public GameObject boomerangObject;
    public Transform boomerangPos;
    private float shotCooldown;
    public float startShotCooldown;
    //private Vector3 lastPosition = new Vector3();
    public float detectRange;

    private void Awake()
    {
        health = maxHealth;
        player = GameObject.FindWithTag("player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        //From EnemyMovementFollow script.
        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
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
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            ring.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        //
    }


    //---------------------------------------------------------------------------------------------------------------------------
    //THIS SECTION HANDLES ENEMY TAKING DAMAGE AND GETTING ITS HEALTH. PULLED FROM Enemy SCRIPT.
    public void Damage(int damageTaken){
        Debug.Log(damageTaken);
        Debug.Log("Taking damage");
        Debug.Log(gameObject.name + ":" + health);
        damageTaken = damageTaken - defense;
        if (damageTaken <= 0) { damageTaken = 1; }
        health = health - damageTaken;
        Debug.Log("Enemy taking: " + damageTaken);
        if (health <= 0){
            Destroy(gameObject);
        }
    }

    public int GetHealth(){
        return health;
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
            slashAttackCon = true;
            AttackColliding();
            StartCoroutine(SlashAttackingContinue());
        }
    }



    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.name == "Player")
        {
            slashAttackCon = false;
        }
    }

    IEnumerator SlashAttackingContinue(){
        while (slashAttackCon == true){
            attacking = true;
            yield return new WaitForSeconds(attackDelay / 2);
            ring.GameObject().GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(attackDelay / 2);
            ring.GameObject().GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponentInParent<PlayerHealth>().Damage(slashDamage);
            attacking = false;
        }
        AttackNotColliding();
    }
    //---------------------------------------------------------------------------------------------------------------------------


}
