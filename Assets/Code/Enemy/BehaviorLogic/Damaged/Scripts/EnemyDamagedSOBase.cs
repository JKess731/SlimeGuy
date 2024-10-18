using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedSOBase : ScriptableObject
{
    [SerializeField] private float _stunTimer = .5f;

    protected EnemyBase _enemy;
    protected GameObject _gameObject;

    private float _timer = 0;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        _gameObject = gameObject;
        _enemy = enemy;
    }
    public virtual void DoEnterLogic() {
        _enemy.State = Enum_State.DAMAGED;
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {

        if(_timer > _stunTimer)
        {
            _enemy.GoToIdle();
        }

        _timer += Time.deltaTime;
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) {}
    public virtual void ResetValues() { 
        _timer = 0;
    }
}
