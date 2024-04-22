using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase:ScriptableObject
{
    public float cooldown;

    public abstract void Activate();
}
