using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Whip", menuName = "Behavior/Whip")]
public class WhipBehavior : AbilitySOBase
{
    [Header("Whip Attributes")]
    [SerializeField] private GameObject _whip;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _rotationSpeed;

    public override void Initialize()
    {
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        GameObject newWhip = Instantiate(_whip, attackPosition, Quaternion.identity);
        newWhip.GetComponent<Whip>().Initialize(_damage, _knockback, _rotationSpeed, _activationTime);

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
