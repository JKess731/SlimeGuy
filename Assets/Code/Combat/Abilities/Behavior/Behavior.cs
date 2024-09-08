using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The SO base for all attack behaviors
/// </summary>
public abstract class Behavior : ScriptableObject
{
    [Header("Timer Attributes")]
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected float _activationTime;
    [SerializeField] protected StatusSO _status;

    public float CooldownTime { get => _cooldownTime; protected set => _cooldownTime = value; }
    public float ActivationTime { get => _activationTime; protected set => _activationTime = value; }

    public StatusSO status { get => _status; protected set => _status = value; }

    public virtual void Initialize()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Activate()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }
}
