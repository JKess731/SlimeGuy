using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSOBase : ScriptableObject
{
    protected EnemyBase _enemy;
    protected Transform _transform;
    protected GameObject _gameObject;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {

        _gameObject = gameObject;
        _transform = gameObject.transform;
        _enemy = enemy;
    }

    public virtual void DoEnterLogic() { 
        _enemy.State = Enum_AnimationState.DEAD;
        _enemy.gameObject.layer = LayerMask.NameToLayer("deadLayer");
        _enemy.KnockBack.StopKnockBack();
    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic(){
        _enemy.MoveEnemy(Vector2.zero);
    }

    public virtual void DoPhysicsLogic() { }

    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }

    public virtual void ResetValues() { }

}
