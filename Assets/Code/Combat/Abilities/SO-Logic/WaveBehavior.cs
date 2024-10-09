using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Wave", menuName = "Behavior/Wave")]
public class WaveBehavior : AbilitySOBase
{
    [Header("Wave Attributes")]

    [SerializeField] private GameObject _wave;

    [Header("Wave Parent")]
    [SerializeField] private int _parentDamage;
    [SerializeField] private float _parentKnockback;
    [SerializeField] private float _parentActivationTime;

    [Space]

    [Header("Wave Child")]
    [SerializeField] private int _childDamage;
    [SerializeField] private float _childKnockback;
    [SerializeField] private float _childActivationTime;

    public override void Initialize()
    {
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        GameObject newWave = Instantiate(_wave, attackPosition, Quaternion.identity);

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
