using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Grenade", menuName = "Attack/Grenade")]
public class GrenadeBehavior : AbilityBaseSO
{
    [Header("Grenade Attributes")]
    [SerializeField] private GameObject _grenade;

    [Header("Prefab Attributes")]
    [SerializeField] private int _grenadeDamage;
    [SerializeField] private float _grenadeKnockback;
    [SerializeField] private float _grenadeSpeed;

    public override void Initialize(AbilityManager abilityManager)
    {
        base.Initialize(abilityManager);
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        Quaternion newRot = rotation;

        GameObject newGrenade = Instantiate(_grenade, attackPosition, newRot);
        newGrenade.GetComponent<Grenade>().Initialize(_grenadeDamage, _grenadeKnockback, _grenadeSpeed);

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
