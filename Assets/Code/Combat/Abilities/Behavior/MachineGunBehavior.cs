using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MachineGun", menuName = "Behavior/MachineGun")]
public class MachineGunBehavior : Behavior
{
    [Header("Machine Gun Attributes")]
    [SerializeField] private GameObject _MachineGun;
    [SerializeField] private GameObject _projectile;

    [Header("Prefab Attributes")]
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;

    private float nextFireTime = 0f; //Tells when to fire
    private MachineGunStruct _MachineGunStruct;
    

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _MachineGunStruct = new MachineGunStruct(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange);
    }


    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.PERFORMING;
        //Debug.Log("Started");
    }
    // Activate the attack
    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        GameObject newMachineGun = Instantiate(_MachineGun, attackPosition, rotation);
        newMachineGun.GetComponent<MachineGun>().SetMachineGunStruct(_MachineGunStruct);

        AbilityState = AbilityState.CANCELING;
    }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.FINISHED;
        //Debug.Log("Finished");
        onBehaviorFinished?.Invoke();
    }
}
    

