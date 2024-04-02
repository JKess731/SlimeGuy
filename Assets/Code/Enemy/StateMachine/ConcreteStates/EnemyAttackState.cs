using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{

    public EnemyAttackState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
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
        enemyBase.animator.SetBool("AttackState", true);
        enemyBase.enemyAttackBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.animator.SetBool("AttackState", false);
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
