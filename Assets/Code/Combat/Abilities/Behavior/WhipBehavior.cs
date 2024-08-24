using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Whip", menuName = "Attack/Whip")]
public class WhipBehavior : Behavior
{
    [Header("Whip Attributes")]
    [SerializeField] private GameObject _whip;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _rotationSpeed;

    private WhipStruct _whipStruct;

    public override void Initialize()
    {
        _whipStruct = new WhipStruct(_damage, _knockback, _activationTime, _rotationSpeed);
    }

    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            GameObject newWhip = Instantiate(_whip, attackPosition, Quaternion.identity);
            newWhip.GetComponent<Whip>().SetWhipStruct(_whipStruct);
        }
    }
}
