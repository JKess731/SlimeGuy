using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    protected EnemyBase _enemy;
    protected Transform _enemyTransform;

    protected GameObject _playerGameObject;
    protected Transform _playerTransform;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        //Get the enemybase and transform components
        _enemy = enemy;
        _enemyTransform = gameObject.transform;

        //Get the player gameobject and transform components
        _playerGameObject = gameObject;
        _playerTransform = GameObject.FindGameObjectWithTag("player").transform;
    }

    public virtual void DoEnterLogic() { 
        _enemy.State = Enum_State.ATTACKING;
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {
        _enemy.MoveEnemy(Vector2.zero);
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType){}
    public virtual void ResetValues() { }
}
