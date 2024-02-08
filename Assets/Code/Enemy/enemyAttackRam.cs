using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyAttackRam : MonoBehaviour
{
    [SerializeField] private int damage;


    //Handles attack collision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("AttackPlayerRam");
        {
            playerHealth playerHealth = collision.gameObject.GetComponent<playerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Attacking");
       
    }
}
