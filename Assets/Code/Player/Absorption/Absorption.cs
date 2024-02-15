using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
/* This is a monobehavior class that is attached onto colliders to give them
 * the absorbption ability.
 * 
 * Stat
 */

public class Absorption : MonoBehaviour
{
    //Created custom enum for absorbtion buff
    enum statBoost {attackUP,hpUP,defenseUP,speedUP}

    [SerializeField] float absorbtionRate = 1f;
    [SerializeField] statBoost _statBoost;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            int enemyHealth = collision.gameObject.GetComponent<enemy>().getHealth();

            if (enemyHealth <= absorbtionRate)
            {
                Debug.Log("absorbed");
                Destroy(collision.gameObject);
            }
        }
    }

    
    private void Absorb(statBoost powerUp)
    {
        switch(powerUp) 
        { 
            case statBoost.attackUP:

                break;
        }
    }
}