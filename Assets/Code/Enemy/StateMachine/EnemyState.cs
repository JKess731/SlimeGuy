using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyState class for the enemy state machine
public class EnemyState
{
    protected EnemyBase _enemyBase;
    protected EnemyStateMachine _enemyStateMachine;

    //Constructor for the creation of the enemy state
    public EnemyState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) { 

        _enemyBase = enemyBase;
        _enemyStateMachine = enemyStateMachine;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType) { }
}
