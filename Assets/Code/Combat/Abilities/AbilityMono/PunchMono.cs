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

    public override void Initialize()
    {
        base.Initialize();
        _playerRb = GameObject.FindWithTag("player").GetComponent<Rigidbody2D>();
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.STARTING;

        Vector2 vecForAng = rotation * Vector2.right;
        _pushDirection = vecForAng; // Use the direction of the punch

        _startPos = _playerRb.transform.position;
        _pushVector = _pushDirection * _pushSpeed;

        GameObject newPunch = Instantiate(_punch, attackPosition, rotation);
        newPunch.GetComponent<Punch>().Initialize(_punchDamage, _punchKnockback, _punchSpeed, _punchRange);

        // Push the player forward
        StartCoroutine(PushPlayerForward());
        StartCoroutine(Cooldown());
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }

    public override void Upgrade(StatsSO playerstats, StatsEnum stat)
    {
        switch (stat)
        {
            //TODO: Implement upgrade logic
        }
    }

    private IEnumerator PushPlayerForward()
    {
        Debug.Log("velocity:" + _playerRb.velocity);
        Debug.Log("push vector:" + _pushVector);
        Debug.Log("startPos:" + _startPos);
        Debug.Log("playerPos:" + _playerRb.transform.position);
        Debug.Log(Vector2.Distance(_startPos, _playerRb.transform.position));

        // Continue pushing the player forward until the range is reached
        while (Vector2.Distance(_startPos, _playerRb.transform.position) < _pushRange)
        {
            Debug.Log("Pushing player forward...");
            // Apply a force in the forward direction
            _playerRb.AddForce(_pushVector * _pushSpeed, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Player pushed forward");
        // Stop player's movement after the push is complete
        _playerRb.velocity = Vector2.zero;
    }
}
