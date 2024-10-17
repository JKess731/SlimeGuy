using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ChaseUntilRange", menuName = "EnemyLogic/ChaseLogic/ChaseUntilRange")]
public class EnemyChaseUntilRange : EnemyChaseSOBase
{
    [SerializeField] private float ChaseSpeed = 1.5f;
    private bool hasLineOfSight = false;

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
            // If the player is outside of shooting or striking distance, chase the player
            if (!_enemy._isWithinShootingDistance && !_enemy._isWithinStikingDistance)
            {
                Vector2 chaseDir = (_playerTransform.position - _enemy.transform.position).normalized;
                _enemy.MoveEnemy(chaseDir * ChaseSpeed);
            }
            else
            {
                // Stop the enemy once it is within range
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
