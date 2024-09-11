using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The SO base for all attack behaviors
/// </summary>
public class Behavior : ScriptableObject, IBehavior
{
    [Header("Timer Attributes")]
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected float _activationTime;
    public float CooldownTime { get => _cooldownTime; protected set => _cooldownTime = value; }
    public float ActivationTime { get => _activationTime; protected set => _activationTime = value; }

    [HideInInspector] protected AbilityState _abilityState;
    public AbilityState AbilityState { get => _abilityState; protected set => _abilityState = value; }
    public virtual void Initialize()
    {
        _abilityState = AbilityState.READY;
       //throw new System.NotImplementedException();
    }
    public virtual void Activate()
    {
      // throw new System.NotImplementedException();
    }
    public virtual void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
       // throw new System.NotImplementedException();
    }

    public virtual void StartBehavior()
    {
        //throw new System.NotImplementedException();
    }
    public virtual void PerformBehavior()
    {
        //throw new System.NotImplementedException();
    }
    public virtual void EndBehavior()
    {
        //throw new System.NotImplementedException();
    }

    public virtual void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        //throw new System.NotImplementedException();
    }
    public virtual void PerformBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        //throw new System.NotImplementedException();
    }
    public virtual void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        //throw new System.NotImplementedException();
    }


    public void SetReady()
    {
        _abilityState = AbilityState.READY;
    }
}
