using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Grenade", menuName = "Attack/Grenade")]
public class GrenadeBehavior : AbilitySOBase
{
    [Header("Grenade Attributes")]
    [SerializeField] private GameObject _grenade;

    [Header("Prefab Attributes")]
    [SerializeField] private int _grenadeDamage;
    [SerializeField] private float _grenadeKnockback;
    [SerializeField] private float _grenadeSpeed;
    [SerializeField] private float _grenadeDistance;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        Quaternion newRot = rotation;

        GameObject newGrenade = Instantiate(_grenade, attackPosition, newRot);
        newGrenade.GetComponent<Grenade>().Initialize(CooldownTime, _grenadeDamage, _grenadeKnockback, _grenadeSpeed, _grenadeDistance);

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
