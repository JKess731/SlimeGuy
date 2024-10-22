using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[CreateAssetMenu(fileName = "RunAwayTeleport", menuName = "EnemyLogic/ChaseLogic/RunAwayTeleport")]

public class EnemyRunAwayTeleport : EnemyMoveSOBase
{
    private bool _teleportfinished = false;
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.TELEPORT && !_enemy.EnemyAnimation.Animator.GetBool("Teleport"))
        {
            OnTeleport();
            Debug.Log("TeleportingStarted");
            return;
        }

        if (triggerType == EnemyBase.AnimationTriggerType.TELEPORT && _enemy.EnemyAnimation.Animator.GetBool("Teleport"))
        {
            _enemy.stateMachine.ChangeState(_enemy.idleState);
            Debug.Log("TeleportingExited");
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

        if (_enemy.EnemyAnimation.Animator.GetBool("Teleport") && _teleportfinished)
        {
            _enemy.MoveEnemy(Vector2.zero);
            return;
        }

        if (_enemy._isWithinTeleportingDistance)
        {
            _enemy.State = Enum_AnimationState.TELEPORTING;
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

    #region Teleport
    private void OnTeleport()
    {
        Vector2 [] teleportPosArray = new Vector2[8];
        float teleportDistance = 5f;
        Vector2 dir = _transform.right * teleportDistance;

        float angleDiff = (360/8);

        for (int i = 0; i < 8; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = Quaternion.Euler(0, 0, addedOffset);

            LayerMask wallMask = LayerMask.GetMask("Wall");

            Ray2D ray = new Ray2D(_transform.position, newRot * _transform.up);


            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, teleportDistance, wallMask);
            Debug.DrawRay(ray.origin, ray.direction * teleportDistance, Color.cyan, 1f);

            //if (hit)
            //{
            //    //if (hit2.collider.CompareTag("wall"))
            //    //{
            //        //Debug.DrawRay(ray.origin, hit.point, Color.green, 1f);
            //        Debug.Log("Hit");
            //    //}
            //}

            if (!hit)
            {
                Debug.DrawRay(ray.origin, ray.direction * teleportDistance, Color.red, 1f);
                teleportPosArray[i] = ray.origin + ray.direction * teleportDistance;
            }
        }

        //foreach (Vector2 pos in teleportPosArray)
        //{
        //    Debug.Log("Teleport Pos: " + pos);
        //}

        Vector2 teleportPos = teleportPosArray[Random.Range(0, teleportPosArray.Length)];
        //Debug.Log("Teleported to: " + teleportPos);
        _transform.position = teleportPos;
        _enemy.EnemyAnimation.Animator.SetBool("Teleport", true);
    }
    #endregion
}
