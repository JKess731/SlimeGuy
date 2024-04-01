using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackSlam : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackDelay;
    [SerializeField] GameObject player;
    public PlayerMove playerMove;
    public GameObject enemy;
    private bool attackCon = false;
    public GameObject ring;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        playerMove = GameObject.FindWithTag("player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        Vector3 rotation = player.transform.position - ring.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        ring.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    //Handles attack collision

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            enemy.GetComponentInParent<EnemyMovementFollow>().AttackColliding();
            attackCon = true;
            StartCoroutine(AttackingContinue());
        }

    }

    IEnumerator AttackingContinue() {
        while (attackCon == true) {
            yield return new WaitForSeconds(attackDelay/2);
            ring.GameObject().GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(attackDelay/2);
            ring.GameObject().GetComponent<SpriteRenderer>().enabled = true;            
            player.GetComponentInParent<PlayerHealth>().Damage(damage);
            StartCoroutine(PlayerStunned());
        }
        
    }

    IEnumerator PlayerStunned() {
        playerMove.input.Disable();
        Debug.Log("Player Stunned");
        yield return new WaitForSeconds(attackDelay/2);
        playerMove.input.Enable();
    }
}
