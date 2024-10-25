using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[CreateAssetMenu(fileName = "BasicDeath", menuName = "EnemyLogic/DeathLogic/BasicDeath")]

public class EnemyBasicDeath : EnemyDeathSOBase
{
    public override void DoAnimationTriggerEventLogic(Enum_AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
        Destroy(_gameObject);
        EnemyBase e = _gameObject.GetComponent<EnemyBase>();
        e.OnDeath?.Invoke();
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        RuntimeManager.PlayOneShot(_enemy.deathSoundEffects[0], _enemy.transform.position);
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
