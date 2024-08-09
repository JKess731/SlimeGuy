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
    [Header("Wave Parent")]
    [SerializeField] WaveParent _parentWave;
    [SerializeField] int _parentDamage;
    [SerializeField] float _parentKnockback;
    [SerializeField] float _parentLifetime;

    [Space]
    [Header("Wave Child")]
    [SerializeField] WaveChild _childWave;
    [SerializeField] int _childDamage;
    [SerializeField] float _childKnockback;
    [SerializeField] float _childLifetime;

    public override void Initialize()
    {
        _parentWave.SetWaveStruct(new WaveStruct(_parentDamage, _parentKnockback, _parentLifetime));
        _childWave.SetWaveStruct(new WaveStruct(_childDamage, _childKnockback, _childLifetime));
    }

    public override void ActivateAttack(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        Instantiate(_parentWave, attackPosition, rotation);
    }
}
