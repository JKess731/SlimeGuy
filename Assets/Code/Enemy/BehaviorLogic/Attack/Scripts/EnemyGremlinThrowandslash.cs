using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GremlinThrowandSlash", menuName = "EnemyLogic/AttackLogic/GremlinThrowandSlash")]
public class EnemyGremlinThrowandslash : EnemyAttackSOBase
{
    [SerializeField] private float _shootingAttackTime = 2f;
    [SerializeField] private float _shootingExitTime = 2f;
    [Space]

    [Header("Melee Attack Attribute")]
    [SerializeField] private GameObject slashTriggerPrefab;
    [SerializeField] private int frontSlashDamage;

    [Header("Ranged Attack Attribute")]
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;

    private Transform attackPoint;
    private GameObject ring;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        Vector3 rotation = _playerTransform.position - ring.transform.position;
        float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        ring.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);

        if (triggerType == EnemyBase.AnimationTriggerType.RangeAttack)
        {
            GremlinShoot();
            if (_enemy.attackSoundEffects.Count > 1)
            {
                RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[1], _enemy.transform.position);
            }
        }

        if (triggerType == EnemyBase.AnimationTriggerType.Attack)
        {
            GremlinSlash();
            if (_enemy.attackSoundEffects.Count > 0)
            {
                RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[0], _enemy.transform.position);
            }
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        attackPoint = _enemy.transform.GetChild(1).GetChild(0);
        ring = _enemy.transform.GetChild(1).gameObject;

        if (_enemy._isWithinShootingDistance)
        {
            _attackTime = _shootingAttackTime;
            _attackExitTime = _shootingExitTime;
        }
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
            _enemy.State = Enum_State.RANGEDATTACK;
            _enemy.MoveEnemy(Vector2.zero);
        }

        //if player is within striking distance do the melee attack
        if (_enemy._isWithinStikingDistance)
        {
            _enemy.State = Enum_State.ATTACKING;
            _enemy.MoveEnemy(Vector2.zero);
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
        Vector2 dir = (_playerTransform.position - _enemy.transform.position).normalized;
        Rigidbody2D bullet = GameObject.Instantiate(bulletPrefab, _enemy.transform.position, Quaternion.identity);
        bullet.velocity = dir * bulletSpeed;
    }

    public void GremlinSlash()
    {
        Instantiate(slashTriggerPrefab, attackPoint.position, ring.transform.rotation);
    }
}