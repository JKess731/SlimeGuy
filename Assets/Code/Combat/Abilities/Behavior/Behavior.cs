using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The SO base for all attack behaviors
/// </summary>
public abstract class Behavior : ScriptableObject, IBehavior
{
    [Header("Timer Attributes")]
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected float _activationTime;

    public float CooldownTime { get => _cooldownTime; protected set => _cooldownTime = value; }
    public float ActivationTime { get => _activationTime; protected set => _activationTime = value; }

    protected AbilityBase _abilityBase;
    public virtual void Initialize(AbilityBase abilityBase)
    {
        _abilityBase = abilityBase;
    }
    public virtual void Activate()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }
    public virtual void StartBehavior(Vector2 attackPosition, Quaternion rotation) { }
    public virtual void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }
    public virtual void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }
}
