using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyAttackRam : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] GameObject player;
    private bool attackCon = false;


    //Handles attack collision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") {
            Debug.Log("Enter the puddles");
            player.GetComponentInParent<playerHealth>().Damage(damage);
            attackCon = true;
            StartCoroutine(attackingContinue());
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Exit the puddles");
            attackCon = false;

        }

    }

    IEnumerator attackingContinue() {
        while (attackCon == true) {
            Debug.Log("continue attacking");
            player.GetComponentInParent<playerHealth>().Damage(damage);
            yield return new WaitForSeconds(.5f);
        }
        
    }
}
