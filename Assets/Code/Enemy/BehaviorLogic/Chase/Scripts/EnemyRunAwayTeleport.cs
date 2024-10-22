using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[CreateAssetMenu(fileName = "RunAwayTeleport", menuName = "EnemyLogic/ChaseLogic/RunAwayTeleport")]

public class EnemyRunAwayTeleport : EnemyMoveSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.TELEPORT && !_enemy.EnemyAnimation.Animator.GetBool("Teleport"))
        {
            OnTeleport();
            return;
        }

        if (triggerType == EnemyBase.AnimationTriggerType.TELEPORT && _enemy.EnemyAnimation.Animator.GetBool("Teleport"))
        {
            _enemy.stateMachine.ChangeState(_enemy.idleState);
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        _enemy.EnemyAnimation.Animator.SetBool("Teleport", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        //If the player is within the shooting distance, change to attack state
        //If not, move towards the player

        //If the player is within the striking distance, attempt to run away
        //If the player is within the teleporting distance, teleport
        //If the player is within the teleporting distance, teleport
        if (_enemy._isWithinTeleportingDistance)
        {
            StartTeleport();
            _enemy.MoveEnemy(Vector2.zero);
            return;
        }

        if (_enemy._isWithinShootingDistance)
        {
            _enemy.stateMachine.ChangeState(_enemy.attackState);
        }

        if (!_enemy._isWithinShootingDistance)
        {
            Vector2 dir = (_playerTransform.position - _transform.position).normalized;
            _enemy.MoveEnemy(dir * _enemy.Stats.GetStat(Enum_Stats.SPEED));
        }

        //If the player is within the striking distance, attempt to run away
        if (_enemy._isWithinStikingDistance)
        {
            Vector2 runDir = -(_playerTransform.position - _transform.position).normalized;
            _enemy.MoveEnemy(runDir * _enemy.Stats.GetStat(Enum_Stats.SPEED));
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

    private void StartTeleport()
    {
        _enemy.State = Enum_AnimationState.TELEPORTING;
    }

    #region Teleport
    private void OnTeleport()
    {
        _enemy.EnemyAnimation.Animator.SetBool("Teleport", true);

        Vector2 [] teleportPosArray = new Vector2[8];
        float teleportDistance = 5f;
        Vector2 dir = _transform.right * teleportDistance;

        float angleDiff = (360/8);

        for (int i = 0; i < 8; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = Quaternion.Euler(0, 0, addedOffset);

            LayerMask wallMask = LayerMask.GetMask("Wall");
            Ray ray = new Ray(_transform.position, newRot * dir);

            if (Physics.Raycast(ray,out RaycastHit hit, wallMask))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Debug.Log("Wall");
                    continue;
                }

                teleportPosArray[i] = ray.origin + ray.direction * teleportDistance;

                Debug.Log("Raycasting");
            }
        }

        Vector2 teleportPos = teleportPosArray[Random.Range(0, teleportPosArray.Length)];
        _transform.position = teleportPos;
        Debug.Log("Teleporting");
    }
    #endregion
}
