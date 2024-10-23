using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseDirectChase", menuName = "EnemyLogic/ChaseLogic/DirectChase")]

public class EnemyChaseDirectToPlayer : EnemyMoveSOBase
{
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
        Vector2 moveDirection = (_playerTransform.position - _enemy.transform.position).normalized;
        _enemy.MoveEnemy(moveDirection * _enemy.Stats.GetStat(Enum_Stats.SPEED));

        //If the player is within the striking distance, change to attack state
        //If the player is within the shooting distance, change to attack state
        if (_enemy._isWithinStikingDistance)
        {
            _enemy.stateMachine.ChangeState(_enemy.attackState);
        }

        if (_enemy._isWithinShootingDistance)
        {
            _enemy.stateMachine.ChangeState(_enemy.attackState);
        }
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
