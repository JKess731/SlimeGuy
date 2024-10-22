using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSOBase : ScriptableObject
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
    }

    public virtual void DoEnterLogic() { 
        _enemy.State = Enum_AnimationState.MOVING;
    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
        //Debug.Log("Chase State");
        Debug.DrawRay(_transform.position, _playerTransform.position - _transform.position, Color.red);
        _enemy.FaceDir  = (_playerTransform.position - _enemy.transform.position).normalized;
    }

    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) {}
    public virtual void ResetValues() { }
}
