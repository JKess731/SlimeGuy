using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseSOBase : ScriptableObject
{
    protected EnemyBase enemy;
    protected Transform transform;
    protected GameObject gameObject;
    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy)
    {

        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;

    }

    public virtual void DoEnterLogic() { }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
        if (enemy.isWithinStikingDistance)
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
        }
    }

    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }

    public virtual void ResetValues() { }
}
