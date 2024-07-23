using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AttackBehavior : ScriptableObject
{
    private float activationTime;

    public virtual void Activate(Quaternion rotation, Vector2 attackPosition)
    {
        throw new System.NotImplementedException();
    }
}
