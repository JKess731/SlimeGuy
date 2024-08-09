using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The SO base for all attack behaviors
/// </summary>
public abstract class AttackBehavior : ScriptableObject
{
    public virtual void Initialize()
    {
        throw new System.NotImplementedException();
    }
    public virtual void ActivateAttack(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

}
