using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform playerTransform;
    private float movementSpeed = 1.75f;

    public EnemyChaseState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;

    }

    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Vector2 moveDirection = (playerTransform.position - enemyBase.transform.position).normalized;
        enemyBase.MoveEnemy(moveDirection *  movementSpeed);

        if(enemyBase.isWithinStikingDistance)
        {
            enemyBase.stateMachine.ChangeState(enemyBase.attackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
