using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStruct
{
    private int _damage;
    private float _knockback;
    private float _activationTime;
    private float _speed;
    private float _distance;
    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float ActivationTime { get { return _activationTime; } }
    public float Speed { get { return _speed; } }
    public float Distance { get { return _distance; } }

    public PushStruct(int damage, float knockback, float activationTime, float speed, float distance)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
        _speed = speed;
        _distance = distance;
    }
}
