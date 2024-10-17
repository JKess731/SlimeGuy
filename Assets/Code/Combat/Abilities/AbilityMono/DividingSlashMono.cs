using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DividingSlashMono : AbilityMonoBase
{
    [Header("Dividing Slash Attributes")]
    [SerializeField] private GameObject _dividingSlash;
    [SerializeField] private int _slashCount = 1;
    [SerializeField] private float _spreadAngle = 45f;

    [SerializeField] private float _pushRange = 5f;   // The range to push the player forward
    [SerializeField] private float _pushSpeed = 10f;  // Speed of the player's forward push

    [Header("Prefab Attributes")]
    [SerializeField] private int _dividingSlashDamage;
    [SerializeField] private float _dividingSlashKnockback;
    [SerializeField] private float _dividingSlashSpeed;
    [SerializeField] private float _dividingSlashRange;

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
        _pushDirection = vecForAng; // Use the direction of the slash
        _playerRb.velocity = new Vector2(1000,1000);

        _startPos = _playerRb.transform.position;
        _pushVector = _pushDirection * _pushSpeed;

        if(_slashCount == 1)
        {
            GameObject newDividingSlash = Instantiate(_dividingSlash, attackPosition, rotation);
            newDividingSlash.GetComponent<DividingSlash>().Initialize(_dividingSlashDamage, _dividingSlashKnockback, _dividingSlashSpeed, _dividingSlashRange);
        }

        // Calculate the angle step based on the number of slashes and the total spread angle

        if (_slashCount > 1)
        {

            float angleDiff = _spreadAngle * 2 / (_slashCount - 1);

            for (int i = 0; i < _slashCount; i++)
            {
                // Calculate the new rotation for each slash based on the current angle
                float addedOffset = -angleDiff * i;
                Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);

                GameObject newDividingSlash = Instantiate(_dividingSlash, attackPosition, newRot);
                newDividingSlash.GetComponent<DividingSlash>().Initialize(_dividingSlashDamage, _dividingSlashKnockback, _dividingSlashSpeed, _dividingSlashRange);

            }
        }

        // Push the player forward
        //StartCoroutine(PushPlayerForward());
        StartCoroutine(Cooldown());
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation){}

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation){}

    public override void Upgrade(StatsSO playerstats, StatsEnum stat)
    {

    }

    private IEnumerator PushPlayerForward()
    {
        Debug.Log("velocity:" + _playerRb.velocity);
        Debug.Log("push vector:" + _pushVector);
        Debug.Log("startPos:" + _startPos);
        Debug.Log("playerPos:" + _playerRb.transform.position);
        Debug.Log(Vector2.Distance(_startPos, _playerRb.transform.position));

        // Push the player forward for a short duration
        while (Vector2.Distance(_startPos, _playerRb.transform.position) < _pushRange)
        {
            Debug.Log("Pushing");
            _playerRb.velocity = _pushVector;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Player pushed forward");
        // Stop player's forward movement after the push
        _playerRb.velocity = Vector2.zero;
    }

}
