using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
/* This is a monobehavior class that is attached onto colliders to give them
 * the absorbption ability.
 */

public class Absorption : MonoBehaviour
{
    [SerializeField] float absorbtionRate = 1f;
    [SerializeField] int growthRate = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            int enemyHealth = collision.gameObject.GetComponent<Enemy>().GetHealth();

            if (enemyHealth <= absorbtionRate)
            {
                //playerStatUI.setText(_statBoost);
                Destroy(collision.gameObject);
            }
        }
    }
}