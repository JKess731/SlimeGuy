using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "GremlinProjectile", menuName = "EnemyLogic/AttackLogic/GremlinProjectile")]
public class EnemyGremlinRangedAttack : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private float bulletSpeed = 10f;

    private float attackCooldownTimer;
    private bool canShoot;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        if (attackCooldownTimer >= timeBetweenShots)
        {
            base.DoAnimationTriggerEventLogic(triggerType);
            if (triggerType == EnemyBase.AnimationTriggerType.Attack)
            {
                GremlinThrow();
                attackCooldownTimer = 0f; 

                if (_enemy.attackSoundEffects.Count > 0)
                {
                    RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[0], _enemy.transform.position);
                }
            }
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _enemy.State = Enum_State.RANGEDATTACK;
        attackCooldownTimer = timeBetweenShots; 
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        attackCooldownTimer += Time.deltaTime; 
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
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

    public void GremlinThrow()
    {
        Vector2 dir = (_playerTransform.position - _enemy.transform.position).normalized;
        Rigidbody2D bullet = GameObject.Instantiate(bulletPrefab, _enemy.transform.position, Quaternion.identity);
        Debug.Log("bullet shot");
        bullet.velocity = dir * bulletSpeed;
    }
}