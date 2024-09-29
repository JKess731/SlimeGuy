using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedSOBase : ScriptableObject
{
    protected EnemyBase _enemy;
    protected Transform _transform;
    protected GameObject _gameObject;

    /*
    // Array to hold multiple of one sound event
    [SerializeField] private EventReference[] GolemDamagedSounds;
    */

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        _gameObject = gameObject;
        _transform = gameObject.transform;
        _enemy = enemy;

        /*
        // Initialize the attackSounds array using FmodEvents instance
        FmodEvents fmodEvents = FmodEvents.instance;

        GolemDamagedSounds = new EventReference[]
        {
            fmodEvents.GolemDamage1,
            fmodEvents.GolemDamage2,
            fmodEvents.GolemDamage3,
            fmodEvents.GolemDamage4,
            fmodEvents.GolemDamage5,
            fmodEvents.GolemDamage6
        };
        */
    }

    public virtual void DoEnterLogic() {
        _enemy.State = Enum_State.DAMAGED;
    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic() { }

    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) {
        if (triggerType == EnemyBase.AnimationTriggerType.GolemDamaged)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.GolemDamage, _transform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfDamaged)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfHurt, _transform.position);
        }

    }

    /*
    public virtual void PlayRandomDamageSound()
    {
        if (GolemDamagedSounds.Length > 0)
        {

            int randomIndex = Random.Range(0, GolemDamagedSounds.Length);
            Debug.Log(randomIndex);
            AudioManager.instance.PlayOneShot(GolemDamagedSounds[randomIndex], _transform.position);
        }
        else
        {
            Debug.LogWarning("No damaged sounds for Golem");
        }
    }
    */

    public virtual void ResetValues() { }
}
