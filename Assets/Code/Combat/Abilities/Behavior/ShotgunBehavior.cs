using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Behavior/Shotgun")]
public class ShotgunBehavior : Behavior
{
    [Header("Shotgun Attributes")]
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private GameObject _projectile;

    [Header("Prefab Attributes")]
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;
    [SerializeField] private int _piercingAmount;
    [SerializeField] private int _bulletBounce;

    private BulletStruct _bulletStruct;

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _bulletStruct = new BulletStruct(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange, status, _piercingAmount, _bulletBounce);
    }
    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        Debug.Log("Started");
        float angleDiff = _spreadAngle * 2 / (_bulletCount - 1);
        for (int i = 0; i < _bulletCount; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);

            GameObject newBullet = Instantiate(_projectile, attackPosition, newRot);
            newBullet.GetComponent<Bullet>().SetBulletStruct(_bulletStruct);
        }

        AbilityState = AbilityState.PERFORMING;
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        Debug.Log("Performed");
        AbilityState = AbilityState.CANCELING;
    }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        Debug.Log("Finished");
        AbilityState = AbilityState.FINISHED;
        onBehaviorFinished?.Invoke();
    }

    public override void Upgrade(StatsSO playerstats, StatsEnum stat)
    {
        switch (stat)
        {
            case StatsEnum.BULLET_COUNT:
                _bulletCount += (int)playerstats.GetStat(StatsEnum.BULLET_COUNT);
                break;
            case StatsEnum.SPREAD_ANGLE:
                _spreadAngle += playerstats.GetStat(StatsEnum.SPREAD_ANGLE);
                break;
            case StatsEnum.PROJECTILE_DAMAGE:
                _projectileDamage += (int)playerstats.GetStat(StatsEnum.PROJECTILE_DAMAGE);
                break;
            case StatsEnum.PROJECTILE_KNOCKBACK:
                _projectileKnockback += playerstats.GetStat(StatsEnum.PROJECTILE_KNOCKBACK);
                break;
            case StatsEnum.PROJECTILE_SPEED:
                _projectileSpeed += playerstats.GetStat(StatsEnum.PROJECTILE_SPEED);
                break;
            case StatsEnum.PROJECTILE_RANGE:
                _projectileRange += playerstats.GetStat(StatsEnum.PROJECTILE_RANGE);
                break;
            case StatsEnum.RICHOCHET_COUNT:
                _bulletBounce += (int)playerstats.GetStat(StatsEnum.RICHOCHET_COUNT);
                break;
            case StatsEnum.PIERCING_COUNT:
                _piercingAmount += (int)playerstats.GetStat(StatsEnum.PIERCING_COUNT);
                break;
        }

    }




}
