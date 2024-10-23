using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipMono : AbilityMonoBase
{
    [Header("Whip Attributes")]
    [SerializeField] private GameObject _whip;

    [Header("Prefab Attributes")]
    [SerializeField] private float _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _activationTime;
    [SerializeField] private float _range;
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

        float addedDamage = _playerStats.playerStats.GetStat(StatsEnum.ATTACK);
        float addedKnockback = _playerStats.playerStats.GetStat(StatsEnum.KNOCKBACK);
        float addedActivationTime = _playerStats.playerStats.GetStat(StatsEnum.ACTIVATION_TIME);
        float addedRotationSpeed = _playerStats.playerStats.GetStat(StatsEnum.ROTATION_SPEED);

        //Instantiate the whip prefab
        GameObject newWhip = Instantiate(_whip,attackPosition, rotation);
        newWhip.GetComponent<Whip>().Initialize( _damage + addedDamage, _knockback + addedKnockback, _activationTime + addedActivationTime, 
            _rotationSpeed + addedRotationSpeed, _range, status);

        Debug.Log("Damage is: " + (_damage + addedDamage));

        StartCoroutine(UiManager.instance.TextAndSliderAdjustment(this, UIAbilityType, _activationTime));
        StartCoroutine(Cooldown());
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation){}

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation){}

    //Override the Cooldown method to add the activation time
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
