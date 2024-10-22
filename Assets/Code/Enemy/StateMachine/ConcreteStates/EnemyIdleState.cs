using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{

    public EnemyIdleState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        _enemyBase.enemyIdleBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        _enemyBase.enemyIdleBaseInstance.DoEnterLogic();
        _enemyBase.State = Enum_AnimationState.IDLING;
    }

    public override void ExitState()
    {
        base.ExitState();
        _enemyBase.enemyIdleBaseInstance.DoExitLogic();

    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _enemyBase.enemyIdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _enemyBase.enemyIdleBaseInstance.DoPhysicsLogic();

    }
}
