using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningState : EnemyState
{
    public EnemySpawningState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enum_AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        _enemyBase.enemySpawnBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        _enemyBase.enemySpawnBaseInstance.DoEnterLogic();
        //_enemyBase.State = Enum_State.SPAWNING;
    }

    public override void ExitState()
    {
        base.ExitState();
        _enemyBase.enemySpawnBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _enemyBase.enemySpawnBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _enemyBase.enemySpawnBaseInstance.DoPhysicsLogic();
    }
}
