using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "RunAwayTeleport", menuName = "EnemyLogic/ChaseLogic/RunAwayTeleport")]

public class EnemyRunAwayTeleport : EnemyMoveSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
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

        //If the player is within the striking distance, change to attack state
        //Else move towards the player
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

        //If the player is within the teleporting distance, teleport
        if (_enemy._isWithinTeleportingDistance)
        {
            Teleport();
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

    private void Teleport()
    {
        _enemy.State = Enum_AnimationState.TELEPORTING;
        Vector2 teleportPos = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        _transform.position = teleportPos;
    }
}
