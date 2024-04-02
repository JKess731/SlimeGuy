using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionWave : MonoBehaviour
{
    UltSlimeWave controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<UltSlimeWave>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            controller.enemies.Add(collision.gameObject);
        }
    }
}
