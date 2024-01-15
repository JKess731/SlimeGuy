using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class lightAbility : ability
{

    public override void Activate(GameObject parent)
    {
        Debug.Log("light");
    }

    public override void Attack()
    {
        base.Attack();
    }
}
