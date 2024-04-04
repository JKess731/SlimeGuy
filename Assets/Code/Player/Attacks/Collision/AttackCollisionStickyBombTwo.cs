using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionStickyBombTwo : MonoBehaviour
{
    ASlimeStickyBomb controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<ASlimeStickyBomb>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy" && controller.throwEnded)
        {
            controller.enemiesInBlastRadius.Add(collision.gameObject);
        }
    }
}
