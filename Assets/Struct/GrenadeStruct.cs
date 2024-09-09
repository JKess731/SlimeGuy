using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The struct for the grenade
/// </summary>
public struct GrenadeStruct
{
    private int _damage;
    private float _knockback;
    private float _grenadeSpeed;

    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float GrenadeSpeed { get { return _grenadeSpeed; } }


    public GrenadeStruct(int damage, float knockback, float bulletSpeed)
    {
        _damage = damage;
        _knockback = knockback;
        _grenadeSpeed = bulletSpeed;
    }
}
