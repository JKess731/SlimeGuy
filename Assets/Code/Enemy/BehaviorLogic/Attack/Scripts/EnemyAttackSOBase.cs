using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Diag = System.Diagnostics;
using UnityEngine;
using System;

public class EnemyAttackSOBase : ScriptableObject
{
    [SerializeField] private float _attackTime = 1f;        //Duration of the attack --> Time of the attack animation
    [SerializeField] private float _attackExitTime = 1f;    //Duration of the attack recovery/exit time

    protected EnemyBase _enemy;
    protected Transform _enemyTransform;
    protected GameObject _playerGameObject;
    protected Transform _playerTransform;

    protected float _timer = 0;
    protected float _exitTimer = 0;
    protected bool _isAttackDone = false;

    private Diag.Stopwatch _stopwatch = new Diag.Stopwatch();

    /// <summary>
    /// Initialize the scriptable object
    /// </summary>
    /// <param name="gameObject"> The enemy game object</param>
    /// <param name="enemy"> The enemybase scripy</param>
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
        _enemy.State = Enum_AnimationState.ATTACKING;
        _stopwatch.Start();
    }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {

        if (_timer > _attackTime && !_isAttackDone)
        {
            _isAttackDone = true;

            //Debug.Log("Stopwatch:" + _stopwatch.Elapsed);
            //Debug.Log("Attack done:" + _timer);
        }

        if (_isAttackDone)
        {
            _exitTimer += Time.deltaTime;

            if (_exitTimer > _attackExitTime)
            {
                //Debug.Log("Stopwatch:" + _stopwatch.Elapsed);
                //Debug.Log("Exit done:" + _exitTimer);
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
        _stopwatch.Stop();
        _timer = 0;
        _exitTimer = 0f;
        _isAttackDone = false;
    }
}
