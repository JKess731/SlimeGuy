using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The struct for all bullet based attacks
/// </summary>
public struct BulletStruct
{
    public int _damage;
    public float _knockback;
    public float _bulletSpeed;
    public float _range;
    private StatusSO _status;
    public int _piercingAmount;
    public int _bulletBounce;

    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float BulletSpeed { get { return _bulletSpeed; } }
    public float Range { get { return _range; } }

    public StatusSO Status { get { return _status; } }

    public int piercingAmount { get { return _piercingAmount; } }
    public int bulletBounce { get { return _bulletBounce; } }

    public BulletStruct(int damage, float knockback, float bulletSpeed, float range, StatusSO status, int piercing, int bounce)
    {
        _damage = damage;
        _knockback = knockback;
        _bulletSpeed = bulletSpeed;
        _range = range;
        _status = status;
        _piercingAmount = piercing;
        _bulletBounce = bounce;
    }

}
