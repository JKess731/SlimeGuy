using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDrop : MonoBehaviour
{
    //Might want to randomize the amount of absorption the player gets
    [SerializeField] private int absorptionAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Destroy(gameObject);
        }
    }
}
