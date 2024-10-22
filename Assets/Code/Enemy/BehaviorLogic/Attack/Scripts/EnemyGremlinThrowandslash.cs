using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GremlinThrowandSlash", menuName = "EnemyLogic/AttackLogic/GremlinThrowandSlash")]
public class EnemyGremlinThrowandslash : EnemyAttackSOBase
{
    [SerializeField] private GameObject slashTriggerPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int frontSlashDamage;
    [SerializeField] private float frontSlashAttackDelay;
    public GameObject ring;

    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private float distanceToCountExit = 3f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float rangedAttackDistance = 5f;

    [SerializeField] private float shootingCooldown = 2f; 
    private float nextShootTime = 0f;

    [SerializeField] private float meleeCooldown = 1.5f; 
    private float nextMeleeAttackTime = 0f;

    private bool isThrow;
    private bool isSlash;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        Vector3 rotation = _playerTransform.position - ring.transform.position;
        float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        ring.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);

        if (triggerType == EnemyBase.AnimationTriggerType.Attack)
        {
            if (isThrow)
            {
                GremlinShoot();
                if (_enemy.attackSoundEffects.Count > 1)
                {
                    RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[1], _enemy.transform.position);
                }
            }
            else if (isSlash)
            {
                GremlinSlash();
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
        attackPoint = _enemy.transform.GetChild(1).GetChild(0);
        ring = _enemy.transform.GetChild(1).gameObject;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        //if the player is within shooting distance and enough time has gone by, enemy shoots
        if (_enemy._isWithinShootingDistance)
        {  
            isThrow = true;
        }

        //if player is within striking distance do the melee attack
        if (_enemy._isWithinStikingDistance)
        {
            isThrow = false;
            isSlash = true;
        }
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

    public void GremlinShoot()
    {
        _enemy.State = Enum_State.RANGEDATTACK;
        Vector2 dir = (_playerTransform.position - _enemy.transform.position).normalized;
        Rigidbody2D bullet = GameObject.Instantiate(bulletPrefab, _enemy.transform.position, Quaternion.identity);
        bullet.velocity = dir * bulletSpeed;
    }

    public void GremlinSlash()
    {
        _enemy.State = Enum_State.ATTACKING;
        Instantiate(slashTriggerPrefab, attackPoint.position, ring.transform.rotation);
    }
}