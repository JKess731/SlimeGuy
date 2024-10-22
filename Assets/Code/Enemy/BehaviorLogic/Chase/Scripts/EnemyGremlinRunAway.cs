using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GremlinRunAway", menuName = "EnemyLogic/ChaseLogic/GremlinRunAway")]
public class EnemyGremlinRunAway : EnemyChaseSOBase
{
    [SerializeField] private float runAwaySpeed = 1.5f;
    [SerializeField] private float changeDirectionCooldown = 0.5f;  
    [SerializeField] private float attackRange = 5.0f; 

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.Move)
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
        _enemy.MoveEnemy(Vector2.zero);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // If the enemy is within run-away distance, move away from the player
        if (_enemy._isWithinRunAwayDistance)
        {
            Vector2 runDir = (_transform.position - _playerTransform.position).normalized;
            _enemy.MoveEnemy(runDir * runAwaySpeed);
        }
        // If the enemy is within shooting distance, attack
        else if (_enemy._isWithinShootingDistance)
        {
          _enemy.stateMachine.ChangeState(_enemy.attackState);
        }
        // If the player is outside both distances, stand still
        else
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

