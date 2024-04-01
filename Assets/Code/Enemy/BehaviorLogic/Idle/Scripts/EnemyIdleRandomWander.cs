using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleRandomWander", menuName = "EnemyLogic/IdleLogic/RandomWander")]

public class EnemyIdleRandomWander : EnemyIdleSOBase
{

    [SerializeField] private float randomMovementRange = 5f;
    [SerializeField] private float randomMovementSpeed = 1f;
    private Vector3 targetPos;
    private Vector3 direction;
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        targetPos = GetRandomPointInCircle();
        Debug.Log("EnterWander");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        direction = (targetPos - enemy.transform.position).normalized;
        enemy.MoveEnemy(direction * randomMovementSpeed);
        if ((enemy.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetPos = GetRandomPointInCircle();
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

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * randomMovementRange;
    }
}
