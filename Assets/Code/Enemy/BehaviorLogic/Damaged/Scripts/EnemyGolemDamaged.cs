using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GolemDamaged", menuName = "EnemyLogic/DamageLogic/GolemDamaged")]
public class EnemyGolemDamaged : EnemyDamagedSOBase
{
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
        if (triggerType == EnemyBase.AnimationTriggerType.GolemDamaged)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.GolemDamage, _transform.position);
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

