using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionPush : MonoBehaviour
{
    UltSlimePush controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<UltSlimePush>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            controller.enemies.Add(collision.gameObject);
        }
    }
}
