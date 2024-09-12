using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The orbit behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Orbit", menuName = "Attack/Orbit")]
public class OrbitBehavior : Behavior
{
    [Header("Orbit Attributes")]
    [SerializeField] private GameObject _orbit;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _distance;

    private OrbitStruct _orbitStruct;

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _orbitStruct = new OrbitStruct(_damage, _knockback, _activationTime, _rotationSpeed, _distance);
    }

    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            GameObject newOrbit = Instantiate(_orbit, attackPosition, Quaternion.identity);
            newOrbit.GetComponent<Orbit>().SetOrbitStruct(_orbitStruct);
        }
    }
}
