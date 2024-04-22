using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(fileName = "DamageAbility", menuName = "Abilities/DamageAbility")]
public class DamageAbility : AbilityBase
{
    public float damage;
    public float range;
    public float absorption;
    public override void Activate()
    {
        throw new System.NotImplementedException();
    }
}
