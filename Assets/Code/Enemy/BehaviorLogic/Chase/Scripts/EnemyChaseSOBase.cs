using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseSOBase : ScriptableObject
{
    protected EnemyBase _enemy;
    protected Transform _transform;
    protected GameObject _gameObject;
    protected Transform _playerTransform;

    /*
    // Array to hold multiple of one sound event
    [SerializeField] private EventReference[] dwarfStepSounds;
    [SerializeField] private EventReference[] GolemStepSounds;
    */

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {

        _gameObject = gameObject;
        _transform = gameObject.transform;
        _enemy = enemy;
        _playerTransform = GameObject.FindGameObjectWithTag("player").transform;

        /*
        // Initialize the attackSounds array using FmodEvents instance
        FmodEvents fmodEvents = FmodEvents.instance;

        dwarfStepSounds = new EventReference[]
        {
            fmodEvents.DwarfStep1,
            fmodEvents.DwarfStep2,
            fmodEvents.DwarfStep3
        };

        GolemStepSounds = new EventReference[]
        {
            fmodEvents.GolemStep1,
            fmodEvents.GolemStep2,
            fmodEvents.GolemStep3
        };
        */

    }

    public virtual void DoEnterLogic() { 
        _enemy.State = Enum_State.MOVING;
    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
        Debug.DrawRay(_transform.position, _playerTransform.position - _transform.position, Color.red);
        _enemy.FaceDir  = (_playerTransform.position - _enemy.transform.position).normalized;

        if (_enemy.isWithinStikingDistance)
        {
            _enemy.stateMachine.ChangeState(_enemy.attackState);
        }
    }

    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) {

    }



    /*public virtual void PlayRandomDwarfStep()
    {
        if (dwarfStepSounds.Length > 0)
        {

            int randomIndex = Random.Range(0, dwarfStepSounds.Length);
            //Debug.Log(randomIndex);
            AudioManager.instance.PlayOneShot(dwarfStepSounds[randomIndex], _transform.position);
        }
        else
        {
            Debug.LogWarning("No step sounds assigned to the dwarf.");
        }
    }

    public virtual void PlayRandomGolemStep()
    {
        if (GolemStepSounds.Length > 0)
        {

            int randomIndex = Random.Range(0, GolemStepSounds.Length);
            //Debug.Log(randomIndex);
            AudioManager.instance.PlayOneShot(GolemStepSounds[randomIndex], _transform.position);
        }
        else
        {
            //Debug.LogWarning("No step sounds assigned to the Golem.");
        }
    }
    */


    public virtual void ResetValues() { }
}
