using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSOBase : ScriptableObject
{
    protected EnemyBase _enemy;
    protected GameObject _gameObject;
    public virtual void Initialize(GameObject gameObject, EnemyBase enemy){
        _enemy = enemy;
        _gameObject = gameObject;
    }
    public virtual void DoEnterLogic() {
        _enemy.State = Enum_AnimationState.SPAWNING;
        _enemy.stateMachine.ChangeState(_enemy.idleState);
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {}
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
