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

        float newDamage = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.ATTACK) + _projectileDamage;
        float newKnockback = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.KNOCKBACK) + _projectileKnockback;
        float newSpeed = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.PROJECTILE_SPEED) + _projectileSpeed;
        float addedPiercingAmount = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.PIERCING_COUNT);
        float addedBulletBounce = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.RICHOCHET_COUNT);

        float angleDiff = _spreadAngle * 2/ (_bulletCount - 1);
        for (int i = 0; i < _bulletCount; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);

            GameObject newBullet = Instantiate(_projectile, attackPosition, newRot);
            newBullet.GetComponent<Bullet>().Initialize(newDamage, newKnockback, newSpeed, _projectileRange, _piercingAmount + (int)addedPiercingAmount, 
                _bulletBounce + (int)addedBulletBounce);
        }

        Debug.Log("Shotty damage: " + newDamage);
        Debug.Log("Shotty knockback: " + newKnockback);

        StartCoroutine(Cooldown());

        //This is basically saying pass in this monobehavior as the ability, use the UIAbility type variable to determine which box it's in in the UI, and 
        //its activation time. This will be the same in every Mono class that calls this, though the activation time parameter value may differ.
        StartCoroutine(UiManager.instance.TextAndSliderAdjustment(this, UIAbilityType, 0));
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation){}

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation){}
}
