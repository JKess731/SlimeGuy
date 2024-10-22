using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPulseMono : AbilityMonoBase
{
    [Header("Push Attributes")]
    [SerializeField] private GameObject _pushPulse;

    [Header("Prefab Attributes")]
    [SerializeField] private float _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _activationTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    private PlayerStateMachine _playerStats;
    private string UIAbilityType;

    public override void Initialize()
    {
        base.Initialize();
        _playerStats = PlayerStats.instance.playerStateMachine;
        UIAbilityType = AbilityManager.Instance.AbilityUIType(this);
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.STARTING;

        float addedDamage = _playerStats.playerStats.GetStat(Enum_Stats.ATTACK);
        float addedKnockback = _playerStats.playerStats.GetStat(Enum_Stats.KNOCKBACK);
        float addedActivationTime = _playerStats.playerStats.GetStat(Enum_Stats.ACTIVATION_TIME);
        float addedSpeed = _playerStats.playerStats.GetStat(Enum_Stats.SPEED);

        GameObject newPushPulse = Instantiate(_pushPulse, attackPosition, Quaternion.identity);
        newPushPulse.GetComponent<PushPulse>().Initialize(_damage + addedDamage, _knockback + addedKnockback, _activationTime + addedActivationTime, 
            _speed + addedSpeed, _distance);

        StartCoroutine(UiManager.instance.TextAndSliderAdjustment(this, UIAbilityType, _activationTime));
        StartCoroutine(Cooldown());
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation){}

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation){}

    public override IEnumerator Cooldown()
    {
        //diag.Stopwatch stopWatch = new diag.Stopwatch();
        //stopWatch.Start();
        yield return new WaitForSeconds(_activationTime);

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
}
