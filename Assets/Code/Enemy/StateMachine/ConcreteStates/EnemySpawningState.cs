using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningState : EnemyState
{
    public EnemySpawningState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        enemyBase.enemySpawnBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.animator.SetBool("SpawnState", true);
        enemyBase.enemySpawnBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.animator.SetBool("SpawnState", false);
        enemyBase.enemySpawnBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.enemySpawnBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.enemySpawnBaseInstance.DoPhysicsLogic();
    }
}
