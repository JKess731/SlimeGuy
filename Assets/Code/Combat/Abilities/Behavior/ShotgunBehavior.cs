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

    private BulletStruct _bulletStruct;

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _bulletStruct = new BulletStruct(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange);
    }
    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {

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
        AbilityState = AbilityState.CANCELING;
    }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.FINISHED;
        onBehaviorFinished?.Invoke();
    }
}
