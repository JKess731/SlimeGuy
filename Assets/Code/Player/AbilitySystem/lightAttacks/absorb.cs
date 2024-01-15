using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class absorb : lightAbility
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    public override void Activate(GameObject parent)
    {
        Attack();
    }

    public override void Attack()
    {
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit: " + enemy.name);
        }
    }

    void onDrawGizmoSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        //Draws collision of attack
        Handles.color = Color.red;
        Handles.DrawWireDisc(attackPoint.position, new Vector3(0, 0, 1), attackRange);
    }
}
