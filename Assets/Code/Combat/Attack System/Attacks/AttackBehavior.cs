using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The SO base for all attack behaviors
/// </summary>
public abstract class AttackBehavior : ScriptableObject
{
    private float activationTime;

    public virtual void ActivateAttack(Quaternion rotation, Vector2 attackPosition)
    {
        throw new System.NotImplementedException();
    }
}
