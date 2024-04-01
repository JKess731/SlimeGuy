using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState
{
    protected Minion minion;
    protected MinionStateMachine stateMachine;

    public MinionState(Minion minion, MinionStateMachine stateMachine)
    {
        this.minion = minion;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Minion.AnimationTriggerType animType) { }
}
