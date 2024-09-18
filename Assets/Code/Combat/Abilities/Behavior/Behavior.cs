using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using diag = System.Diagnostics;

/// <summary>
/// The SO base for all attack behaviors
/// </summary>
public class Behavior : ScriptableObject, IBehavior
{
    [Header("Timer Attributes")]
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected float _activationTime;
    [SerializeField] protected StatusSO _status;
    public float CooldownTime { get => _cooldownTime; protected set => _cooldownTime = value; }
    public float ActivationTime { get => _activationTime; protected set => _activationTime = value; }

    public StatusSO status { get => _status; protected set => _status = value; }

    [HideInInspector] protected AbilityState _abilityState;
    public AbilityState AbilityState { get => _abilityState; protected set => _abilityState = value; }

    protected AbilityBase _abilityBase;

    public delegate void OnBehaviorFinished();
    public OnBehaviorFinished onBehaviorFinished;

    public virtual void Initialize(AbilityBase abilityBase)
    {
        _abilityBase = abilityBase;
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
    public virtual void CancelBehavior()
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

    public virtual void Upgrade(StatsSO playerStats)
    {

    }

    public virtual IEnumerator Cooldown()
    {
        diag.Stopwatch stopWatch = new diag.Stopwatch();
        stopWatch.Start();

        Debug.Log("Cooldown Started: " + _cooldownTime);
        _abilityState = AbilityState.COOLDOWN;

        yield return new WaitForSeconds(_cooldownTime);

        // Get the elapsed time as a TimeSpan value.
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds,
        ts.Milliseconds / 10);
        Debug.Log("RunTime " + elapsedTime);

        _abilityState = AbilityState.READY;
        Debug.Log("Cooldown Finished");
    }
}

public enum AbilityState
{
    READY,
    STARTING,
    PERFORMING,
    CANCELING,
    FINISHED,
    COOLDOWN
}