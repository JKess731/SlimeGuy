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
        enemyBase.enemyIdleBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.animator.SetBool("IdleState", true);
        enemyBase.enemyIdleBaseInstance.DoEnterLogic();

    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.animator.SetBool("IdleState", false);
        enemyBase.enemyIdleBaseInstance.DoExitLogic();

    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.enemyIdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.enemyIdleBaseInstance.DoPhysicsLogic();

    }
}
