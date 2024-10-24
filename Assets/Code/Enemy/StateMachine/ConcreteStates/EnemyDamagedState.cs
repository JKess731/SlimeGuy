using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedState : EnemyState
{
    public EnemyDamagedState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enum_AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        _enemyBase.enemyDamagedBaseInstance.DoAnimationTriggerEventLogic(triggerType);

    }

    public override void EnterState()
    {
        base.EnterState();
        _enemyBase.enemyDamagedBaseInstance.DoEnterLogic();
        _enemyBase.State = Enum_AnimationState.DAMAGED;
    }

    public override void ExitState()
    {
        base.ExitState();
        _enemyBase.enemyDamagedBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _enemyBase.enemyDamagedBaseInstance.DoFrameUpdateLogic();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _enemyBase.enemyDamagedBaseInstance.DoPhysicsLogic();
    }
}
