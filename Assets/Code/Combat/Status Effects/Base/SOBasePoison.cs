using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonDamage", menuName = "Status Effects/Poison")]
public class SOBasePoison : Effect
{
    public float damage;
    public int intervals;
    public float totalTime;

    public override void ApplyModifier(GameObject target)
    {
        EnemyBase enemyBase = target.GetComponent<EnemyBase>();
        enemyBase.Damage(damage);
    }
}
