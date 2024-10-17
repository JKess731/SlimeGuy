using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedSOBase : ScriptableObject
{
    protected EnemyBase _enemy;
    protected GameObject _gameObject;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        _gameObject = gameObject;
        _enemy = enemy;
    }
    public virtual void DoEnterLogic() {
        _enemy.State = Enum_State.DAMAGED;
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) {}
    public virtual void ResetValues() { }
}
