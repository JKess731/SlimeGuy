using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jared Kessler

/**
 * 
 * In the future use this class as the attack collision class
 * Change ASlimeWhipControl to AttackControl or whatever it becomes
 * 
 */

public class AttackCollision : MonoBehaviour
{
    ASlimeWhipControl controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<ASlimeWhipControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            controller.enemies.Add(collision.gameObject);
        }
    }
}
