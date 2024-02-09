using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorption : MonoBehaviour
{
    public float absorbtionRate = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("enemy");
            int enemyHealth = collision.gameObject.GetComponent<enemy>().getHealth();

            if (enemyHealth <= absorbtionRate)
            {
                Debug.Log("absorbed");
                Destroy(collision.gameObject);
            }
        }
    }
}