using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PunchMono : AbilityMonoBase
{
    [Header("Punch Attributes")]
    [SerializeField] private GameObject _punch;

    [SerializeField] private float _pushRange = 5f;   // The range to push the player forward
    [SerializeField] private float _pushSpeed = 10f;  // Speed of the player's forward push

    [Header("Prefab Attributes")]
    [SerializeField] private int _punchDamage;
    [SerializeField] private float _punchKnockback;
    [SerializeField] private float _punchSpeed;
    [SerializeField] private float _punchRange;

    private Rigidbody2D _playerRb;  // Rigidbody of the player
    private Vector2 _pushDirection; // Direction to push the player
    private Vector2 _startPos;
    private Vector2 _pushVector;

    private PlayerStateMachine _playerStats;
    private string UIAbilityType;

    public override void Initialize()
    {
        base.Initialize();
        _playerStats = PlayerStats.instance.playerStateMachine;
        UIAbilityType = AbilityManager.Instance.AbilityUIType(this);
        _playerRb = GameObject.FindWithTag("player").GetComponent<Rigidbody2D>();
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.STARTING;

        Vector2 vecForAng = rotation * Vector2.right;
        _pushDirection = vecForAng; // Use the direction of the punch

        _startPos = _playerRb.transform.position;
        _pushVector = _pushDirection * _pushSpeed;

        float newDamage = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.ATTACK) + _punchDamage;
        float newKnockback = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.KNOCKBACK) + _punchKnockback;
        float newSpeed = _playerStats.playerStats.ModifiedStatValue(Enum_Stats.PROJECTILE_SPEED) + _punchSpeed;

        GameObject newPunch = Instantiate(_punch, attackPosition, rotation);
        newPunch.GetComponent<Punch>().Initialize((int)newDamage, newKnockback, newSpeed, _punchRange);

        Debug.Log("punch damage: " + newDamage);
        Debug.Log("punch knockback: " + newKnockback);

        // Push the player forward
        StartCoroutine(PushPlayerForward());
        StartCoroutine(Cooldown());

        //This is basically saying pass in this monobehavior as the ability, use the UIAbility type variable to determine which box it's in in the UI, and 
        //its activation time. This will be the same in every Mono class that calls this, though the activation time parameter value may differ.
        StartCoroutine(UiManager.instance.TextAndSliderAdjustment(this, UIAbilityType, 0));
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }

    private IEnumerator PushPlayerForward()
    {

        // Continue pushing the player forward until the range is reached
        while (Vector2.Distance(_startPos, _playerRb.transform.position) < _pushRange)
        {
            // Apply a force in the forward direction
            _playerRb.AddForce(_pushVector * _pushSpeed, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }
        // Stop player's movement after the push is complete
        _playerRb.velocity = Vector2.zero;
    }
}
