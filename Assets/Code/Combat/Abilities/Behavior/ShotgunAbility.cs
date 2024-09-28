using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Ability/Shotgun")]
public class ShotgunAbilitySO : AbilityBaseSO
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

    public override void Initialize(AbilityManager abilityManager)
    {
        base.Initialize(abilityManager);
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        //Debug.Log("Started");
        float angleDiff = _spreadAngle * 2 / (_bulletCount - 1);
        for (int i = 0; i < _bulletCount; i++)
        {
            float addedOffset = -angleDiff * i;
            Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);

            GameObject newBullet = Instantiate(_projectile, attackPosition, newRot);
            newBullet.GetComponent<Bullet>().Initialize(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange, _piercingAmount, _bulletBounce);
        }

        AbilityState = AbilityState.PERFORMING;
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        //Debug.Log("Performed");

        //AbilityManager.StartCoroutine(Cooldown());


        AbilityState = AbilityState.CANCELING;
    }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        //Debug.Log("Finished");
        AbilityState = AbilityState.FINISHED;
        onBehaviorFinished?.Invoke();
    }

    public override void Upgrade(StatsSO playerstats, StatsEnum stat)
    {
        switch (stat)
        {
            case StatsEnum.ATTACK:
                _projectileDamage += (int) playerstats.GetStat(StatsEnum.ATTACK);
                Debug.Log("Projectile Damage Upgraded: " + _projectileDamage);
                break;
            case StatsEnum.KNOCKBACK:
                _projectileKnockback += playerstats.GetStat(StatsEnum.KNOCKBACK);
                break;
            case StatsEnum.SPEED:
                _projectileSpeed += playerstats.GetStat(StatsEnum.SPEED);
                break;
        }
    }
}
