using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WhipStruct
{
    private int _damage;
    private float _knockback;
    private float _activationTime;
    private float _rotationSpeed;
    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float ActivationTime { get { return _activationTime; } }
    public float RotationSpeed { get { return _rotationSpeed; } }

    public WhipStruct(int damage, float knockback, float activationTime, float rotationSpeed)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
        _rotationSpeed = rotationSpeed;
    }
}
