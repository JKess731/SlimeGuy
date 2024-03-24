using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform playerTransform;
    private float timer;
    private float timeBetweenShots = 2f;
    private float exitTimer;
    private float timeTillExit = 3f;
    private float distanceToCountExit = 3f;
    private float bulletSpeed = 10f;

    public EnemyAttackState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
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
        enemyBase.MoveEnemy(Vector2.zero);
        if(timer > timeBetweenShots)
        {
            timer = 0f;
            Vector2 dir = (playerTransform.position - enemyBase.transform.position).normalized;
            Rigidbody2D bullet = GameObject.Instantiate(enemyBase.bulletPrefab, enemyBase.transform.position, Quaternion.identity);
            bullet.velocity = dir * bulletSpeed;
        }
        if (Vector2.Distance(playerTransform.position, enemyBase.transform.position) > distanceToCountExit)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer > timeTillExit)
            {
                enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
            }
        }
        else {
            exitTimer = 0f;
        }
        timer += Time.deltaTime;
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
