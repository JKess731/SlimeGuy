using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public Effects effectTypes { get; set; }

    public abstract void ApplyModifier(GameObject target);
}
