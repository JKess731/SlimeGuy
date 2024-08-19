using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Wave", menuName = "Attack/Wave")]
public class WaveBehavior : AttackBehavior
{
    [Header("Wave Attributes")]

    [Space]
    [SerializeField] private GameObject _wave;

    [Header("Wave Parent")]
    [SerializeField] private int _parentDamage;
    [SerializeField] private float _parentKnockback;
    [SerializeField] private float _parentLifetime;

    [Space]
    [Header("Wave Child")]
    [SerializeField] private int _childDamage;
    [SerializeField] private float _childKnockback;
    [SerializeField] private float _childLifetime;

    private WaveStruct _parentStruct;
    private WaveStruct _childStruct;

    public override void Initialize()
    {
        _parentStruct = new WaveStruct(_parentDamage, _parentKnockback, _parentLifetime);
        _childStruct = new WaveStruct(_childDamage, _childKnockback, _childLifetime);
    }

    public override void ActivateAttack(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        _wave.GetComponent<Wave>().Spawn(attackPosition, rotation, _parentStruct, _childStruct);
    }
}
