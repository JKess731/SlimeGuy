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

    /*
    // Array to hold multiple of one sound event
    [SerializeField] private EventReference[] golemAttackSounds;
    */

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {

        _gameObject = gameObject;
        _transform = gameObject.transform;
        _enemy = enemy;
        _playerTransform = GameObject.FindGameObjectWithTag("player").transform;

        // Initialize the attackSounds array using FmodEvents instance
        /*
        FmodEvents fmodEvents = FmodEvents.instance;

        golemAttackSounds = new EventReference[]
        {
            fmodEvents.GolemAttack1,
            fmodEvents.GolemAttack2,
            fmodEvents.GolemAttack3
        };
        */

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

        if (triggerType == EnemyBase.AnimationTriggerType.GolemAttack)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.GolemAttack, _transform.position);
        }
    }


    /*
    public virtual void PlayRandomGolemAttack()
    {
        if (golemAttackSounds.Length > 0)
        {

            int randomIndex = Random.Range(0, golemAttackSounds.Length);
            Debug.Log(randomIndex);
            AudioManager.instance.PlayOneShot(golemAttackSounds[randomIndex], _transform.position);
        }
        else
        {
            Debug.LogWarning("No attack sounds assigned to Golem.");
        }
    }
    */

    public virtual void ResetValues() { }
}
