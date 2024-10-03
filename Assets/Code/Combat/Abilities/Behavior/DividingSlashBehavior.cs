using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "DividingSlash", menuName = "Attack/Dividing Slash")]
public class DividingSlashBehavior : AbilityBaseSO
{
    [Header("Dividing Slash Attributes")]
    [SerializeField] private GameObject _dividingSlash;
    [SerializeField] private int _slashCount = 1;
    [SerializeField] private float _spreadAngle = 45f;

    [Header("Prefab Attributes")]
    [SerializeField] private int _dividingSlashDamage;
    [SerializeField] private float _dividingSlashKnockback;
    [SerializeField] private float _dividingSlashSpeed;
    [SerializeField] private float _dividingSlashRange;

    public override void Initialize(AbilityManager abilityManager)
    {
        base.Initialize(abilityManager);
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        // Calculate the angle step based on the number of slashes and the total spread angle
        float angleStep = _spreadAngle / (_slashCount - 1);
        float currentAngle = -_spreadAngle / 2;  // Start from the negative half of the spread

        Vector2 averageDirection = Vector2.zero;

        for (int i = 0; i < _slashCount; i++)
        {
            // Calculate the new rotation for each slash based on the current angle
            Quaternion newRot = rotation * Quaternion.Euler(0, 0, currentAngle);

            GameObject newDividingSlash = Instantiate(_dividingSlash, attackPosition, newRot);
            newDividingSlash.GetComponent<DividingSlash>().Initialize(_dividingSlashDamage, _dividingSlashKnockback, _dividingSlashSpeed, _dividingSlashRange);

            AbilityState = AbilityState.PERFORMING;
        }
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