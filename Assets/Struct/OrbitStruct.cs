using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OrbitStruct
{
    private int _damage;
    private float _knockback;
    private float _activationTime;
    private float _rotationSpeed;
    private float _distance;
    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float ActivationTime { get { return _activationTime; } }
    public float RotationSpeed { get { return _rotationSpeed; } }
    public float Distance { get { return _distance; } }

    public OrbitStruct(int damage, float knockback, float activationTime, float rotationSpeed, float distance)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
        _rotationSpeed = rotationSpeed;
        _distance = distance;
    }
}
