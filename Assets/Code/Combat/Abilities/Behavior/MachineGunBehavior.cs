using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MachineGun", menuName = "Attack/MachineGun")]
public class MachineGunBehavior : Behavior
{
    [Header("Machine Gun Attributes")]
    [SerializeField] private int _bulletCount = 15;
    [SerializeField] private float _fireRate = 0.1f; // Time between shots
    [SerializeField] private GameObject _projectile;

    [Header("Prefab Attributes")]
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;

    private BulletStruct _bulletStruct;
    private float _lastFireTime; // Tracks the last time the gun fired
    private float _firingDuration = 5f; //Fire Timer
    private float _timeLeftToFire = 0f;
    private bool _isFiring = false; 

    public override void Initialize()
    {
        _bulletStruct = new BulletStruct(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange);
        _lastFireTime = -_fireRate; // Ensures immediate firing
    }

    // Activate the attack
    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started && !_isFiring) 
        {
            _timeLeftToFire = _firingDuration; // Start the 5-second timer
            _isFiring = true; //Firing Loop
            Debug.Log("Machine gun firing started for 5 seconds");
        }

        // If the gun is currently in the firing loop
        if (_isFiring)
        {
            while (_timeLeftToFire > 0)
            {
                //Reduce Time
                _timeLeftToFire -= Time.deltaTime;
                _lastFireTime = Time.deltaTime; // Update the last fire time

                // Fire a bullet
                GameObject newBullet = Instantiate(_projectile, attackPosition, rotation);
                newBullet.GetComponent<Bullet>().SetBulletStruct(_bulletStruct);

                Debug.Log("Bullet Fired");
            }
        }

        // Once the time has run out, stop firing
        if (_timeLeftToFire <= 0)
        {
            _isFiring = false; // End the firing loop
            Debug.Log("Machine gun firing ended");
        }
    }
}
    

