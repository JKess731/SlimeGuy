using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GremlinProjectile", menuName = "EnemyLogic/AttackLogic/GremlinProjectile")]
public class EnemyGremlinRangedAttack : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private float timeTillExit = 3f;
    [SerializeField] private float distanceToCountExit = 3f;
    [SerializeField] private float bulletSpeed = 10f;

    private float timer;
    private float exitTimer;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.ATTACK)
        {
            if (_enemy.attackSoundEffects.Count > 0)
            {
                RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[0], _enemy.transform.position);
            }
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _enemy.State = Enum_AnimationState.RANGEDATTACK;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        //_enemy.MoveEnemy(Vector2.zero);
        if (timer > timeBetweenShots)
        {
            timer = 0f;
            Vector2 dir = (_playerTransform.position - _enemy.transform.position).normalized;
            Rigidbody2D bullet = GameObject.Instantiate(bulletPrefab, _enemy.transform.position, Quaternion.identity);
            Debug.Log("bullet shot");
            bullet.velocity = dir * bulletSpeed;
        }
        if (Vector2.Distance(_playerTransform.position, _enemy.transform.position) > distanceToCountExit)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer > timeTillExit)
            {
                _enemy.stateMachine.ChangeState(_enemy.moveState);
            }
        }
        else
        {
            exitTimer = 0f;
        }
        timer += Time.deltaTime;
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, EnemyBase enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}