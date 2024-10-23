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

        float newDamage = _playerStats.playerStats.ModifiedStatValue(StatsEnum.ATTACK) + _damage;
        float newKnockback = _playerStats.playerStats.ModifiedStatValue(StatsEnum.KNOCKBACK) + _knockback;
        float newActivationTime = _playerStats.playerStats.ModifiedStatValue(StatsEnum.ACTIVATION_TIME) + _activationTime;
        float newRotationSpeed = _playerStats.playerStats.ModifiedStatValue(StatsEnum.ROTATION_SPEED) + _rotationSpeed;

        //Instantiate the whip prefab
        GameObject newWhip = Instantiate(_whip,attackPosition,Quaternion.identity);
        newWhip.GetComponent<Whip>().Initialize(newDamage, newKnockback, newActivationTime,
           newRotationSpeed, status);

        //This is basically saying pass in this monobehavior as the ability, use the UIAbility type variable to determine which box it's in in the UI, and 
        //its activation time. This will be the same in every Mono class that calls this, though the activation time parameter value may differ.
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
