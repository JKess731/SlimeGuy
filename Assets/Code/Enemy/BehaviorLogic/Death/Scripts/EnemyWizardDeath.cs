using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "WizardDeath", menuName = "EnemyLogic/DeathLogic/WizardDeath")]
public class EnemyWizardDeath : EnemyDeathSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.Death)
        {
            Debug.Log("WizardDeath");
            if (_enemy.deathSoundEffects.Count > 0)
            {
                RuntimeManager.PlayOneShot(_enemy.deathSoundEffects[0], _enemy.transform.position);
            }
            Destroy(_gameObject);
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
