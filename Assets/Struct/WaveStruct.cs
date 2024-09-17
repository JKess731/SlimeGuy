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
    private StatusSO _status;

    public int Damage { get { return _damage; } }
    public float Knockback { get { return _knockback; } }
    public float ActivationTime { get { return _activationTime; } }
    public StatusSO Status { get { return _status; } }
    public WaveStruct(int damage, float knockback, float activationTime, StatusSO status)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
        _status = status;
    }
}
