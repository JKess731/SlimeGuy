using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : EnemyState
{
    public EnemyDamagedState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        enemyBase.enemyAttackBaseInstance.DoAnimationTriggerEventLogic(triggerType);

    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.enemyAttackBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.enemyAttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.enemyAttackBaseInstance.DoFrameUpdateLogic();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.enemyAttackBaseInstance.DoPhysicsLogic();
    }
}
