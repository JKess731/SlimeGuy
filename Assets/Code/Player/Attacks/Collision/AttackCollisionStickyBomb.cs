using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionStickyBomb : MonoBehaviour
{
    ASlimeStickyBomb controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<ASlimeStickyBomb>();
        Debug.Log(controller.name);
        Debug.Log(controller.enemiesInKockBackRadius);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy" && controller.throwEnded 
            && !controller.enemiesInKockBackRadius.Contains(collision.transform.gameObject))
        {
            Debug.Log("enemy in trig");
            controller.enemiesInKockBackRadius.Add(collision.gameObject);
        }
    }
}
