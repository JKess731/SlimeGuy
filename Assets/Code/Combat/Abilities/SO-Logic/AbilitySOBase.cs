using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using diag = System.Diagnostics;

/// <summary>
/// The SO base for all attack behaviors
/// </summary>
public abstract class AbilitySOBase : ScriptableObject, IAbility
{
    [Header("Ability Attributes")]
    [SerializeField] protected string _abilityName;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected AbilityType _abilityType;


    [Header("Variable Attributes")]
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected float _activationTime;
    [SerializeField] protected StatusSO _status;

    [HideInInspector] protected AbilityState _abilityState;
    [HideInInspector] protected AbilityManager _abilityManager;

    public delegate void OnBehaviorFinished();
    public OnBehaviorFinished onBehaviorFinished { get; set; }

    public string AbilityName { get => _abilityName; }
    public Sprite Icon { get => _icon; }
    public float CooldownTime { get => _cooldownTime;}
    public float ActivationTime { get => _activationTime; protected set => _activationTime = value; }
    public StatusSO status { get => _status; protected set => _status = value; }
    public AbilityState AbilityState { get => _abilityState; set => _abilityState = value; }
    public AbilityType AbilityType { get => _abilityType; }
    public AbilityManager AbilityManager { get => _abilityManager; }

    public virtual void Initialize(){}
    public virtual void StartBehavior(Vector2 attackPosition, Quaternion rotation) {}
    public virtual void PerformBehavior(Vector2 attackPosition, Quaternion rotation) {}
    public virtual void CancelBehavior(Vector2 attackPosition, Quaternion rotation) {}
    public virtual IEnumerator Cooldown()
    {
        //diag.Stopwatch stopWatch = new diag.Stopwatch();
        //stopWatch.Start();

        //Debug.Log("Cooldown Started: " + _cooldownTime);
        _abilityState = AbilityState.COOLDOWN;

        yield return new WaitForSeconds(_cooldownTime);

        // Get the elapsed time as a TimeSpan value.
        //stopWatch.Stop();
        //TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //ts.Hours, ts.Minutes, ts.Seconds,
        //ts.Milliseconds / 10);
        //Debug.Log("RunTime " + elapsedTime);

        _abilityState = AbilityState.READY;
        //Debug.Log("Cooldown Finished");
    }

    public virtual IEnumerator Activate()
    {
        throw new System.NotImplementedException();
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

public enum AbilityType
{
    PRIMARY,
    SECONDARY,
    DASH,
    PASSIVE
}