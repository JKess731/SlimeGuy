using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 targetPos;
    private Vector3 direction;

    public EnemyIdleState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemyBase.isAggroed) {
            enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
        }

        direction = (targetPos - enemyBase.transform.position).normalized;
        enemyBase.MoveEnemy(direction * enemyBase.randomMovementSpeed);
        if ((enemyBase.transform.position - targetPos).sqrMagnitude < 0.01f) {
            targetPos = GetRandomPointInCircle();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector3 GetRandomPointInCircle() {
        return enemyBase.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemyBase.randomMovementRange;
    }
}
