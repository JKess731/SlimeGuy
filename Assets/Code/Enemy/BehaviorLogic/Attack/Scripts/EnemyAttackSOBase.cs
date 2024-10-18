using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    [SerializeField] private float _attackTime = 1f;        //Duration of the attack --> Time of the attack animation
    [SerializeField] private float _attackExitTime = 1f;    //Duration of the attack recovery/exit time

    protected EnemyBase _enemy;
    protected GameObject _playerGameObject;
    protected Transform _playerTransform;

    protected float _timer = 0;
    protected float _exitTimer = 0;
    protected bool _isAttackDone = false;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        //Get the enemybase and transform components
        _enemy = enemy;
        //_enemyTransform = gameObject.transform;

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

        if (_timer > _attackTime)
        {
            _isAttackDone = true;
        }

        if (_isAttackDone)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _attackExitTime)
            {
                _enemy.GoToIdle();
            }
        }
        else
        {
            _exitTimer = 0;
        }

        _timer += Time.deltaTime;
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType){}
    public virtual void ResetValues() { 
        _timer = 0;
        _exitTimer = 0f;
    }
}
