using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackState : MinionState
{
    private GameObject _target;

    public MinionAttackState(Minion minion, MinionStateMachine stateMachine) : base(minion, stateMachine)
    {

    }

    public override void AnimationTriggerEvent(Minion.AnimationTriggerType animType)
    {
        base.AnimationTriggerEvent(animType);
    }

    public override void EnterState()
    {
        base.EnterState();

        _target = stateMachine.target;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public IEnumerator Shoot(float attackDelay)
    {
        // Do Some Attack

        yield return new WaitForSeconds(attackDelay);

        _target.gameObject.SetActive(false);

        if (!_target.activeSelf)
        {
            stateMachine.ChangeState(minion.toPlayerState);
        }

        Debug.Log("Delay over");
    }
}
