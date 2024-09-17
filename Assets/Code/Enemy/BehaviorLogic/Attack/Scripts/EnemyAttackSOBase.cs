using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    protected EnemyBase _enemy;
    protected Transform _transform;
    protected GameObject _gameObject;
    protected Transform _playerTransform;


    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {

        _gameObject = gameObject;
        _transform = gameObject.transform;
        _enemy = enemy;
        _playerTransform = GameObject.FindGameObjectWithTag("player").transform;

    }

    public virtual void DoEnterLogic() { 
        _enemy.State = Enum_State.ATTACKING;
    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic() { }

    public virtual void DoPhysicsLogic() { }

    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBase.AnimationTriggerType.DwarfAttack)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfAttack, _transform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfDamaged)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfHurt, _transform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfDeath)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfDeath, _transform.position);
        }

    }

    public virtual void ResetValues() { }
}
