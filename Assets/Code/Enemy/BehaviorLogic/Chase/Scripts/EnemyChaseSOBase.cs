using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseSOBase : ScriptableObject
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
        _enemy.State = Enum_State.MOVING;
    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
        _enemy.FaceDir  = (_playerTransform.position - _enemy.transform.position).normalized;
        if (_enemy.isWithinStikingDistance)
        {
            _enemy.stateMachine.ChangeState(_enemy.attackState);
        }
    }

    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }

    public virtual void ResetValues() { }
}
