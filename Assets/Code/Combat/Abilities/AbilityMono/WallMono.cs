using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMono : AbilityMonoBase
{
    [Header("Wall Attributes")]
    [SerializeField] private GameObject _wall;

    [Header("Prefab Attributes")]
    [SerializeField] private int _wallDamage;
    [SerializeField] private float _wallKnockback;
    [SerializeField] private float _activationTime;

    private Rigidbody2D _playerRb;
    private Vector2 _startPos;

    private PlayerStateMachine _playerStats;
    private string UIAbilityType;

    public override void Initialize()
    {
        base.Initialize();
        _playerRb = GameObject.FindWithTag("player").GetComponent<Rigidbody2D>();
        _playerStats = PlayerStats.instance.playerStateMachine;
        UIAbilityType = AbilityManager.Instance.AbilityUIType(this);
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.STARTING;

        float addedDamage = _playerStats.playerStats.GetStat(Enum_Stats.ATTACK);
        float addedKnockback = _playerStats.playerStats.GetStat(Enum_Stats.KNOCKBACK);

        _startPos = _playerRb.transform.position;

        GameObject newWall = Instantiate(_wall, attackPosition, rotation);
        newWall.GetComponent<Wall>().Initialize(_wallDamage + addedDamage, _wallKnockback + addedDamage, _activationTime);

        StartCoroutine(Cooldown());
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }

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
