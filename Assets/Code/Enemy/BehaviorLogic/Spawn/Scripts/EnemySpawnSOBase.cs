using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSOBase : ScriptableObject
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

    }

    public virtual void DoEnterLogic() { }

    public virtual void DoExitLogic() { ResetValues(); }

    public virtual void DoFrameUpdateLogic()
    {
       
    }

    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }

    public virtual void ResetValues() { }
}
