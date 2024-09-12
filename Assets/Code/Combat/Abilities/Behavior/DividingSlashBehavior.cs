using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "DividingSlash", menuName = "Attack/Dividing Slash")]
public class DividingSlashBehavior : Behavior
{
    [Header("Dividing Slash Attributes")]
    [SerializeField] private GameObject _dividingSlash;

    [Header("Prefab Attributes")]
    [SerializeField] private int _dividingSlashDamage;
    [SerializeField] private float _dividingSlashKnockback;
    [SerializeField] private float _dividingSlashSpeed;
    [SerializeField] private float _dividingSlashRange;

    private DividingSlashStruct _dividingSlashStruct;

    public override void Initialize(AbilityBase abilitybase)
    {
        base.Initialize(abilitybase);
        _dividingSlashStruct = new DividingSlashStruct(_dividingSlashDamage, _dividingSlashKnockback, _dividingSlashSpeed, _dividingSlashRange);
    }

    //Activate the attack
    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        //if (context.started)
        //{
        //    for (int i = 0; i < 1; i++)
        //    {
        //        Quaternion newRot = rotation;

        //        GameObject newDividingSlash = Instantiate(_dividingSlash, attackPosition, newRot);
        //        newDividingSlash.GetComponent<DividingSlash>().SetDividingSlashStruct(_dividingSlashStruct);
        //    }
        //}
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        Quaternion newRot = rotation;

        GameObject newDividingSlash = Instantiate(_dividingSlash, attackPosition, newRot);
        newDividingSlash.GetComponent<DividingSlash>().SetDividingSlashStruct(_dividingSlashStruct);

        AbilityState = AbilityState.PERFORMING;
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.CANCELING;
    }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.FINISHED;
    }
}