using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Wave", menuName = "Attack/Wave")]
public class WaveBehavior : Behavior
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

    private WaveStruct _parentStruct;
    private WaveStruct _childStruct;

    public override void Initialize()
    {
        _parentStruct = new WaveStruct(_parentDamage, _parentKnockback,  _parentActivationTime, status);
        _childStruct = new WaveStruct(_childDamage, _childKnockback, _childActivationTime, status);
    }

    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            _wave.GetComponent<Wave>().Spawn(attackPosition, rotation, _parentStruct, _childStruct);
        }
    }
}
