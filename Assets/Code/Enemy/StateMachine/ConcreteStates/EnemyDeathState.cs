using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        _enemyBase.enemyDeathBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        _enemyBase.enemyDeathBaseInstance.DoEnterLogic();
        _enemyBase.State = Enum_State.DEAD;
        //Debug.Log("Enemy is dead");
    }

    public override void ExitState()
    {
        base.ExitState();
        _enemyBase.enemyDeathBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _enemyBase.enemyDeathBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _enemyBase.enemyDeathBaseInstance.DoPhysicsLogic();
    }
}
