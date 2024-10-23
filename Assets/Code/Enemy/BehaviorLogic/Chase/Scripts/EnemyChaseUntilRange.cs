using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ChaseUntilRange", menuName = "EnemyLogic/ChaseLogic/ChaseUntilRange")]
public class EnemyChaseUntilRange : EnemyMoveSOBase
{
    [SerializeField] private float ChaseSpeed = 1.5f;
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == Enum_AnimationTriggerType.MOVE)
        {
            if (_enemy.moveSoundEffects.Count > 0)
            {
                RuntimeManager.PlayOneShot(_enemy.moveSoundEffects[0], _enemy.transform.position);
            }
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
        if (_enemy._isWithinShootingDistance)
        {
            _enemy.stateMachine.ChangeState(_enemy.attackState);
        }

        if(!_enemy._isWithinShootingDistance && !_enemy._isWithinStikingDistance)
        {
            _enemy.MoveEnemy(Vector2.zero);
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
}
