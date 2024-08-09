using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The struct for all wave based attacks
/// </summary>
public struct WaveStruct
{
    private int _damage;
    private float _knockback;
    private float _activationTime;

    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float ActivationTime { get { return _activationTime; } }
    public WaveStruct(int damage, float knockback, float activationTime)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
    }
}