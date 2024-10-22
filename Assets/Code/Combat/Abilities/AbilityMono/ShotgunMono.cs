using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMono : AbilityMonoBase
{
    [Header("Shotgun Attributes")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _spreadAngle;

    [Header("Prefab Attributes")]
    [SerializeField] private float _projectileDamage;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;
    [SerializeField] private int _piercingAmount;
    [SerializeField] private int _bulletBounce;

    private PlayerStateMachine _playerStats;
    private string UIAbilityType;

    public override void Initialize()
    {
        
        base.Initialize();
        _playerStats = PlayerStats.instance.playerStateMachine;
        UIAbilityType = AbilityManager.Instance.AbilityUIType(this);
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.STARTING;

        float addedDamage = _playerStats.playerStats.GetStat(Enum_Stats.ATTACK);
        float addedKnockback = _playerStats.playerStats.GetStat(Enum_Stats.KNOCKBACK);
        float addedSpeed = _playerStats.playerStats.GetStat(Enum_Stats.SPEED);
        float addedPiercingAmount = _playerStats.playerStats.GetStat(Enum_Stats.PIERCING_COUNT);
        float addedBulletBounce = _playerStats.playerStats.GetStat(Enum_Stats.RICHOCHET_COUNT);

        Debug.Log("Shotgun Starting");

        float angleDiff = _spreadAngle * 2 / (_bulletCount - 1);
        for (int i = 0; i < _bulletCount; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);

            GameObject newBullet = Instantiate(_projectile, attackPosition, newRot);
            newBullet.GetComponent<Bullet>().Initialize(_projectileDamage + addedDamage, _projectileKnockback + addedKnockback, 
                _projectileSpeed + addedSpeed, _projectileRange, _piercingAmount + (int)addedPiercingAmount, _bulletBounce + (int)addedBulletBounce);
            Debug.Log("Shotgun Damage:" + (_projectileDamage + addedDamage));
        }

        StartCoroutine(Cooldown());
        StartCoroutine(UiManager.instance.TextAndSliderAdjustment(this, UIAbilityType, 0));
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation){}

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation){}
}
