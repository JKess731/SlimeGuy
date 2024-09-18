using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusSO : ScriptableObject
{
    [Header("Status Attributes")]
    [SerializeField] protected float _Timer;
    [SerializeField] protected int _ticksPerSecond;

    public float Timer { get => _Timer; protected set => _Timer = value; }
    public float ticksPerSecond { get => _Timer; protected set => _Timer = value; }

    public virtual void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public virtual IEnumerator Apply(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
