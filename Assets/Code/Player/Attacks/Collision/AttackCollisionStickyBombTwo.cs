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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy" && controller.throwEnded
             && !controller.enemiesInKockBackRadius.Contains(collision.transform.gameObject))
        {
            Debug.Log("enemy in trig");
            controller.enemiesInBlastRadius.Add(collision.gameObject);
        }
    }
}
