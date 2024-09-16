using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The push pulse behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Push Pulse", menuName = "Attack/PushPulse")]
public class PushPulseBehavior : Behavior
{
    [Header("Push Attributes")]
    [SerializeField] private GameObject _pushPulse;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private PushPulseStruct _pushPulseStruct;

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _pushPulseStruct = new PushPulseStruct(_damage,_knockback,_activationTime, _speed, _distance);
    }

    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            GameObject newPushPulse = Instantiate(_pushPulse, attackPosition, Quaternion.identity);
            newPushPulse.GetComponent<PushPulse>().SetPushPulseStruct(_pushPulseStruct);
        }
    }
}