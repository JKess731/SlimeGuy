using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Grenade", menuName = "Attack/Grenade")]
public class GrenadeBehavior : Behavior
{
    [Header("Grenade Attributes")]
    [SerializeField] private GameObject _grenade;

    [Header("Prefab Attributes")]
    [SerializeField] private int _grenadeDamage;
    [SerializeField] private float _grenadeKnockback;
    [SerializeField] private float _grenadeSpeed;


    private GrenadeStruct _grenadeStruct;

    public override void Initialize()
    {
        _grenadeStruct = new GrenadeStruct(_grenadeDamage, _grenadeKnockback, _grenadeSpeed);
    }

    //Activate the attack
    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            for (int i = 0; i < 1; i++)
            {
                Quaternion newRot = rotation;

                GameObject newGrenade = Instantiate(_grenade, attackPosition, newRot);
                newGrenade.GetComponent<Grenade>().SetGrenadeStruct(_grenadeStruct);
            }
        }
    }
}
