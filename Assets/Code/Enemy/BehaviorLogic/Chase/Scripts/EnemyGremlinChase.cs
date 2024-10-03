using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GremlinChase", menuName = "EnemyLogic/ChaseLogic/GremlinChase")]
public class EnemyGremlinChase : EnemyChaseSOBase
{
    [SerializeField] public float movementSpeed = 1.75f;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.PlayDwarfFootStepSound)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfStep, _transform.position);
        }

    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (_enemy.isDead)
        {
            movementSpeed = 0;
        }
        Vector2 moveDirection = (_playerTransform.position - _enemy.transform.position).normalized;
        _enemy.MoveEnemy(moveDirection * movementSpeed);
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
}