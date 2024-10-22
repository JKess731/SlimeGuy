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

    private float timer;
    private float exitTimer;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        Vector3 rotation = _playerTransform.position - ring.transform.position;
        float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        ring.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);

        if (triggerType == EnemyBase.AnimationTriggerType.ATTACK)
        {
            if (_enemy.attackSoundEffects.Count > 0)
            {
                RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[0], _enemy.transform.position);
            }

            float distanceToPlayer = Vector2.Distance(_playerTransform.position, _enemy.transform.position);

            if (distanceToPlayer <= attackRange)
            {
                Instantiate(slashTriggerPrefab, attackPoint.position, ring.transform.rotation);
                timer = 0f; 
            }
            else if (distanceToPlayer <= rangedAttackDistance) 
            {
                if (timer > timeBetweenShots)
                {
                    timer = 0f;
                    _enemy.State = Enum_AnimationState.RANGEDATTACK;
                    Vector2 dir = (_playerTransform.position - _enemy.transform.position).normalized;
                    Rigidbody2D bullet = GameObject.Instantiate(bulletPrefab, _enemy.transform.position, Quaternion.identity);
                    bullet.velocity = dir * bulletSpeed;
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

        timer += Time.deltaTime;

        if (Vector2.Distance(_playerTransform.position, _enemy.transform.position) > distanceToCountExit)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer > timeBetweenShots)
            {
                _enemy.stateMachine.ChangeState(_enemy.moveState);
            }
        }
        else
        {
            exitTimer = 0f;
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
}