using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The push pulse behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Push Pulse", menuName = "Attack/PushPulse")]
public class PushPulseBehavior : AbilitySOBase
{
    [Header("Push Attributes")]
    [SerializeField] private GameObject _pushPulse;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    public override void Initialize()
    {
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        GameObject newPushPulse = Instantiate(_pushPulse, attackPosition, Quaternion.identity);
        newPushPulse.GetComponent<PushPulse>().Initialize(_damage, _knockback, _speed, _distance, _activationTime);

        AbilityState = AbilityState.PERFORMING;
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.CANCELING;
    }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.FINISHED;
        onBehaviorFinished?.Invoke();
    }
}