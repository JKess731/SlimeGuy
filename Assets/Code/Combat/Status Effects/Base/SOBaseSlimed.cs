using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimedSlowness", menuName = "Status Effects/Slimed")]
public class SOBaseSlimed : Effect
{
    public float time;
    public float speedDecrease;

    public override void ApplyModifier(GameObject target)
    {
        EnemyBase enemyBase = target.GetComponent<EnemyBase>();
        
    }
}
