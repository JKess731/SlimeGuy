using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Attack/Shotgun")]
public class ShotgunBehavior : AttackBehavior
{
    [Header("Shotgun Attributes")]
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private GameObject _projectile;

    public override void ActivateAttack(Quaternion rotation, Vector2 attackPosition)
    {
        float angleDiff = _spreadAngle * 2 / (_bulletCount - 1);
        for (int i = 0; i < _bulletCount; i++){
            float addedOffset = -angleDiff * i;
            Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);
            Instantiate(_projectile, attackPosition, newRot);
        }
    }   
}
