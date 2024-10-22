using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Diag = System.Diagnostics;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    [SerializeField] private float _attackTime = 1f;
    [SerializeField] private float _attackExitTime = 1f;

    protected EnemyBase _enemy;
    protected Transform _enemyTransform;
    protected GameObject _playerGameObject;
    protected Transform _playerTransform;

    protected float _timer = 0;
    protected float _exitTimer = 0;
    protected bool _isAttackDone = false;

    private Diag.Stopwatch _stopwatch = new Diag.Stopwatch();

    /// <summary>
    /// Initalize the scroptable object
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="enemy"></param>

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
        _stopwatch.Start();
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {
        if (_timer > _attackTime && !_isAttackDone)
        {
            _isAttackDone = true;
        }

        if (_isAttackDone)
        {
            _exitTimer += Time.deltaTime;

            if(_exitTimer > _attackExitTime)
            {
                _enemy.GoToIdle();
            }
        }
        else
        {
            _exitTimer = 0;
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType){}
    public virtual void ResetValues() { }
}
