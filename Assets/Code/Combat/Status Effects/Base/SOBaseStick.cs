using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StickinessStun", menuName = "Status Effects/Stickiness")]
public class SOBaseStick : Effect
{
    private float time;

    public override void ApplyModifier(GameObject target)
    {
        EnemyBase enemyBase = target.GetComponent<EnemyBase>();
        //enemyBase.ModifyMoveSpeed(-enemyBase.GetSpeed(), time);
    }
}
