using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleSOBase : ScriptableObject
{
    protected EnemyBase enemy;
    protected Transform transform;
    protected GameObject gameObject;
    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy) { 

        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
    }

    public virtual void DoEnterLogic() { 
        enemy.State = Enum_AnimationState.IDLING;
    }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic() {

        // if the enemy is aggroed, change to chase state
        // if the enemy is within shooting distance, change to attack state
        // if the enemy is within striking distance, change to attack state

        // if the enemy is not aggroed, change to idle state
        // Stricking Bool is set in chase state

        if (enemy._isAggroed)
        {
            enemy.stateMachine.ChangeState(enemy.moveState);
        }

        //if (enemy._isWithinStikingDistance)
        //{
        //    enemy.stateMachine.ChangeState(enemy.attackState);
        //}

        //if (enemy._isWithinShootingDistance)
        //{
        //    enemy.stateMachine.ChangeState(enemy.attackState);
        //}
    }

    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }

}
