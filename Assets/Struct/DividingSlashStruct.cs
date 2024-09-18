using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The struct for the dividing slash
/// </summary>
public struct DividingSlashStruct
{
    private int _damage;
    private float _knockback;
    private float _speed;
    private float _range;

    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float Speed { get { return _speed; } }
    public float Range { get { return _range; } }

    public DividingSlashStruct(int damage, float knockback, float speed, float range)
    {
        _damage = damage;
        _knockback = knockback;
        _speed = speed;
        _range = range;
    }
}
