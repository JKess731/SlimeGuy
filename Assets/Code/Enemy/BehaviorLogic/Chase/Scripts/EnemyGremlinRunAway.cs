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

    private Vector2 runDir;  
    private float lastDirectionChangeTime;  

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
        UpdateRunDirection(); 
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (Vector2.Distance(_transform.position, _playerTransform.position) > attackRange)
        {
            _enemy.MoveEnemy(runDir * runAwaySpeed);
        }
        else
        {
            _enemy.MoveEnemy(Vector2.zero);
            _enemy.stateMachine.ChangeState(_enemy.attackState);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            UpdateRunDirection();
            lastDirectionChangeTime = Time.time;
        }
    }

    private void UpdateRunDirection()
    {
        runDir = -(_playerTransform.position - _transform.position).normalized;

        float randomAngle = Random.Range(-90f, 90f);
        runDir = Quaternion.Euler(0, 0, randomAngle) * runDir;
    }
}

