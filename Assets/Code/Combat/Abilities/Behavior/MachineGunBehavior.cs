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
    //[SerializeField] private int _bulletCount = 15;
    [SerializeField] private float _fireRate = 0.1f; // Time between shots
    [SerializeField] private GameObject _projectile;

    [Header("Prefab Attributes")]
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;

    private BulletStruct _bulletStruct;
    private float _firingDuration = 5f; //Fire Timer

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _bulletStruct = new BulletStruct(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange);
    }

    // Activate the attack
    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        //if (context.started && !_isFiring) 
        //{
        //    _isFiring = true; //Firing Loop
        //    Debug.Log("Machine gun firing started for 5 seconds");
        //}

        if (context.started)
        {
            Debug.Log("Started");
            Debug.Log(Time.deltaTime);
        }

        // If the gun is currently in the firing loop
        if (context.performed)
        {
            Debug.Log("Performed");
            Debug.Log(Time.deltaTime);
            //Debug.Log("Machine gun Performed");
            //float currentTime = 0;                      // Get the current time
            //float currentfireRate = _fireRate;          // Time between shots

            //while (currentTime < _firingDuration)
            //{
            //    Debug.Log("Machine gun firing:" + currentTime);
            //    currentTime += Time.deltaTime;          // Update the current time
            //    if (currentTime  >= currentfireRate)
            //    {
            //        // Fire a bullet
            //        Vector2 randomAttackPos = new Vector2(attackPosition.x + Random.Range(-0.5f, 0.5f), attackPosition.y);
            //        GameObject newBullet = Instantiate(_projectile, randomAttackPos, rotation);
            //        newBullet.GetComponent<Bullet>().SetBulletStruct(_bulletStruct);
            //        currentfireRate += _fireRate;           // Increase the fire rate
            //    }
            //}
        }

        if (context.canceled)
        {
            Debug.Log("Canceled");
            Debug.Log(Time.deltaTime);
        }
    }
}
    

