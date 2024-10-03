using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "EnemyLogic/ChaseLogic/Teleport")]
public class EnemyTeleportSO : EnemyChaseSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.WizardTeleportTrigger)
        {
            Debug.Log("WizardTeleport");
            AudioManager.instance.PlayOneShot(FmodEvents.instance.WizardTeleport, _transform.position);
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
        if (_enemy.isWithinShootingDistance)
        {
            _enemy.stateMachine.ChangeState(_enemy.attackState);
        }
        //Vector2 runDir = -(_playerTransform.position - _transform.position).normalized;
        //_enemy.MoveEnemy(runDir * runAwaySpeed);
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
