using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAbilities : ScriptableObject
{
    public float damage;
    public float cooldown;

    public virtual void Activate(Quaternion rotation, Vector2 attackPosition){}
}
