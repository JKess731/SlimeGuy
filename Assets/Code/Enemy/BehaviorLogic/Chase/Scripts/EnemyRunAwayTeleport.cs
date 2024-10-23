using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[CreateAssetMenu(fileName = "RunAwayTeleport", menuName = "EnemyLogic/ChaseLogic/RunAwayTeleport")]

public class EnemyRunAwayTeleport : EnemyMoveSOBase
{
    [SerializeField] private float _teleportDistance = 5f;
    public override void DoAnimationTriggerEventLogic(Enum_AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == Enum_AnimationTriggerType.TELEPORT && !_enemy.EnemyAnimation.Animator.GetBool("Teleport"))
        {
            OnTeleport();
            return;
        }

        if (triggerType == Enum_AnimationTriggerType.TELEPORT && _enemy.EnemyAnimation.Animator.GetBool("Teleport"))
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

        //If the player is within the teleporting distance, teleport
        if (_enemy._isWithinTeleportingDistance)
        {
            _enemy.State = Enum_AnimationState.TELEPORTING;
            _enemy.MoveEnemy(Vector2.zero);
            return;
        }

        //If the player is within the shooting distance, change to attack state
        //If not, move towards the player
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
        //List to store all possible teleport positions
        List<Vector2> teleportPosArray = new List<Vector2>();
        
        //Math to calculate the angle difference between each raycast
        float angleDiff = (360/8);

        for (int i = 0; i < 8; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = Quaternion.Euler(0, 0, addedOffset);

            LayerMask wallMask = LayerMask.GetMask("Wall");

            Ray2D ray = new Ray2D(_transform.position, newRot * _transform.up);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _teleportDistance, wallMask);
            Debug.DrawRay(ray.origin, ray.direction * _teleportDistance, Color.cyan, 1f);

            //Debug.Draw for invalid raycasts
            if (hit)
            {
                //Draw the invalid raycast that hits a wall
                //Skips the rest of the loop
                Debug.DrawRay(ray.origin, ray.direction * _teleportDistance, Color.red, 1f);
                continue;
            }

            //Debug.Draw for valid raycasts
            if (!hit)
            {
                //Add the teleport position to the array if the raycast is does not hit a wall
                Debug.DrawRay(ray.origin, ray.direction * _teleportDistance, Color.green, 1f);
                teleportPosArray.Add(ray.origin + ray.direction * _teleportDistance);
            }
        }

        //If there are no valid teleport positions, teleport to the current position
        if (teleportPosArray.Count == 0)
        {
            teleportPosArray.Add(_transform.position);
        }

        Vector2 teleportPos = teleportPosArray[Random.Range(0, teleportPosArray.Count)];
        _transform.position = teleportPos;
        _enemy.EnemyAnimation.Animator.SetBool("Teleport", true);
    }
    #endregion
}
