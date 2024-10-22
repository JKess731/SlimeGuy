using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GremlinRunAway", menuName = "EnemyLogic/ChaseLogic/GremlinRunAway")]
public class EnemyGremlinRunAway : EnemyMoveSOBase
{
    [SerializeField] private float runAwaySpeed = 1.5f;
    [SerializeField] private float changeDirectionCooldown = 0.5f;  
    [SerializeField] private float attackRange = 5.0f; 

    private Vector2 runDir;
    private bool hasLineOfSight = false;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.MOVE)
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

        hasLineOfSight = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(_enemy.transform.position, _playerTransform.position - _enemy.transform.position);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("player"))
            {
                hasLineOfSight = true;
                Debug.DrawRay(_enemy.transform.position, _playerTransform.position - _enemy.transform.position, Color.green);
                break; 
            }
            else
            {
                Debug.DrawRay(_enemy.transform.position, _playerTransform.position - _enemy.transform.position, Color.red);
            }
        }

        if (hasLineOfSight)
        {
            // If the enemy is within shooting distance, attack
            if (_enemy._isWithinShootingDistance)
            {
                Debug.Log("ATTACK");
                _enemy.stateMachine.ChangeState(_enemy.attackState);
            }
            // If the enemy is within run-away distance, move away from the player
            else if (_enemy._isWithinRunAwayDistance)
            {
                Debug.Log("MOVE");
                Vector2 runDir = (_transform.position - _playerTransform.position).normalized;
                _enemy.MoveEnemy(runDir * runAwaySpeed);
            }
            // If the player is outside both distances, stand still
            else
            {
                Debug.Log("STAND STILL");
                _enemy.MoveEnemy(Vector2.zero);
            }
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

