using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackSlash : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackDelay;
    [SerializeField] GameObject player;
    public GameObject enemy;
    private bool attackCon = false;
    private bool attacking = false;
    public GameObject ring;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }

    void Update()
    {
        if (attacking == false) {
            Vector3 rotation = player.transform.position - ring.transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            ring.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }


    //Handles attack collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            attackCon = true;
            enemy.GetComponentInParent<EnemyMovementFollow>().AttackColliding();
            StartCoroutine(AttackingContinue());
        }
    }

    //Handles attack collision
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            attackCon = false;
        }
    }

    //Handles slash attack delay
    IEnumerator AttackingContinue() {
        while (attackCon == true) {
            attacking = true;
            yield return new WaitForSeconds(attackDelay / 2);
            ring.GameObject().GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(attackDelay / 2);
            ring.GameObject().GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponentInParent<PlayerHealth>().Damage(damage);
            attacking = false;
        }
        enemy.GetComponentInParent<EnemyMovementFollow>().AttackNotColliding();
    }

}
