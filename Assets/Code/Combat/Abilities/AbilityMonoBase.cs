using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMonoBase : MonoBehaviour, IAbility
{
    [Header("Ability Attributes")]
    [SerializeField] protected string _abilityName;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected AbilityType _abilityType;
    [SerializeField] protected EventReference _sfx;

    [Header("Variable Attributes")]
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected StatusSO _status;

    [HideInInspector] protected AbilityState _abilityState;

    public delegate void OnBehaviorFinished();
    public OnBehaviorFinished onBehaviorFinished;

    public string AbilityName { get => _abilityName; }
    public Sprite Icon { get => _icon; }
    public float CooldownTime { get => _cooldownTime; }
    public StatusSO status { get => _status; protected set => _status = value; }
    public AbilityState AbilityState { get => _abilityState; set => _abilityState = value; }
    public AbilityType AbilityType { get => _abilityType; }

    /// <summary>
    /// Sets the ability to be ready to use and sets the gameobject to active
    /// </summary>
    public virtual void Initialize()
    {
        _abilityState = AbilityState.READY;
        gameObject.SetActive(true);
    }

    /// Starts the behavior of the ability - On Pressed
    public virtual void StartBehavior(Vector2 attackPosition, Quaternion rotation) { }

    /// Performs the behavior of the ability - While Helded
    public virtual void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }

    /// Cancels the behavior of the ability - On Release
    public virtual void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }
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

    public virtual void Drop()
    {
        gameObject.SetActive(false);
        //Instantiate(_dropPrefab, transform.position, Quaternion.identity);
    }
}
