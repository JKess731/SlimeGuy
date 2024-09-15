using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Whip", menuName = "Behavior/Whip")]
public class WhipBehavior : Behavior
{
    [Header("Whip Attributes")]
    [SerializeField] private GameObject _whip;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _rotationSpeed;

    private WhipStruct _whipStruct;

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _whipStruct = new WhipStruct(_damage, _knockback, _activationTime, _rotationSpeed);
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        GameObject newWhip = Instantiate(_whip, attackPosition, Quaternion.identity);
        newWhip.GetComponent<Whip>().SetWhipStruct(_whipStruct);

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
