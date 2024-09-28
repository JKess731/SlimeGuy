using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WhipStruct
{
    public int _damage;
    public float _knockback;
    public float _activationTime;
    public float _rotationSpeed;
    public StatusSO _status;
    public int Damage { get { return _damage; } set => _damage = value; }
    public float Knockback { get { return _knockback; } }
    public float ActivationTime { get { return _activationTime; } }
    public float RotationSpeed { get { return _rotationSpeed; } }
    public StatusSO Status { get { return _status; } }

    public WhipStruct(int damage, float knockback, float activationTime, float rotationSpeed, StatusSO status)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
        _rotationSpeed = rotationSpeed;
        _status = status;
    }
}
