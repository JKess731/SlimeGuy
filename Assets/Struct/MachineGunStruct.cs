using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MachineGunStruct
{
    private int _damage;
    private float _knockback;
    private float _bulletSpeed;
    private float _range;

    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float BulletSpeed { get { return _bulletSpeed; } }
    public float Range { get { return _range; } }


    public MachineGunStruct(int damage, float knockback, float bulletSpeed, float range)
    {
        _damage = damage;
        _knockback = knockback;
        _bulletSpeed = bulletSpeed;
        _range = range;
    }
}
