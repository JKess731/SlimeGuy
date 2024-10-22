using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile + Teleport", menuName = "EnemyLogic/AttackLogic/RangeAttack/Projectile + Teleport")]
public class EnemyRangeAttackProjectileTeleport : EnemyAttackSOBase
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _knockbackPower;
    [SerializeField] private float _lifetime;

    private Transform _attackPoint;

    #region State Functions
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        Vector3 dir = _playerTransform.position - _attackPoint.transform.position;
        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (triggerType == EnemyBase.AnimationTriggerType.ATTACK)
        {
            if (_enemy.attackSoundEffects.Count > 0)
            {
                RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[0], _enemy.transform.position);
            }

            GameObject projectile = Instantiate(_projectilePrefab, _attackPoint.position, Quaternion.Euler(0, 0, rotation));
            projectile.GetComponent<EnemyProjectile>().Initialize(_damage, _speed, _knockbackPower, _lifetime);
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _attackPoint = _enemy.Ring.GetChild(0);
        _enemy.MoveEnemy(Vector2.zero);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (_enemy._isWithinTeleportingDistance)
        {
            Debug.Log("Teleporting");
            _enemy.stateMachine.ChangeState(_enemy.moveState);
        }

        if (_enemy._isWithinStikingDistance)
        {
            Vector2 runDir = -(_playerTransform.position - _enemyTransform.position).normalized;
            _enemy.MoveEnemy(runDir * _enemy.Stats.GetStat(Enum_Stats.SPEED));
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
    #endregion
}
