using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseRunAway", menuName = "EnemyLogic/ChaseLogic/RunAway")]

public class EnemyChaseRunAway : EnemyMoveSOBase
{
    [SerializeField] private float runAwaySpeed = 1.5f;
    [SerializeField] private Transform attackPoint;
    public GameObject ring;
    public override void DoAnimationTriggerEventLogic(Enum_AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        Vector2 runDir = -(_playerTransform.position - _transform.position).normalized;
        _enemy.MoveEnemy(runDir *  runAwaySpeed);
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

}
