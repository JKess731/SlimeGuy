using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The push behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Push", menuName = "Attack/Push")]
public class PushBehavior : Behavior
{
    [Header("Push Attributes")]
    [SerializeField] private GameObject _push;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private PushStruct _pushStruct;

    public override void Initialize()
    {
        _pushStruct = new PushStruct(_damage, _knockback, _activationTime, _speed, _distance);
    }

    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            GameObject newPush = Instantiate(_push, attackPosition, Quaternion.identity);
            newPush.GetComponent<Push>().SetPushStruct(_pushStruct);
        }
    }
}
