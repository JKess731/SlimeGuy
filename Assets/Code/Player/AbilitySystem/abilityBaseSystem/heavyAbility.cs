using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class heavyAbility : ability
{
    public override void Activate(GameObject parent)
    {
        Debug.Log("Heavy");
    }

    public override void Attack()
    {
        base.Attack();
    }
}
