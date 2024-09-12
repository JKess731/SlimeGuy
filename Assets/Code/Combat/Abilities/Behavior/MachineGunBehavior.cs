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
    [SerializeField] private int _bulletCount = 15;
    [SerializeField] private float _fireRate = 0.5f; // Time between shots
    [SerializeField] private float _spreadAngle; 
    [SerializeField] private GameObject _projectile;

    [Header("Prefab Attributes")]
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;

    private float nextFireTime = 0f; //Tells when to fire
    private BulletStruct _bulletStruct;
    

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _bulletStruct = new BulletStruct(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange);
    }

    // Activate the attack
    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            // Check if enough time has passed to fire again
            if (Time.time >= nextFireTime)
            {
                Debug.Log("Performed");
                Debug.Log(Time.deltaTime);

                float angleDiff = _spreadAngle * 2 / (_bulletCount - 1);
                for (int i = 0; i < _bulletCount; i++)
                {
                    float addedOffset = -angleDiff * i;
                    Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);

                    GameObject newBullet = Instantiate(_projectile, attackPosition, newRot);
                    newBullet.GetComponent<Bullet>().SetBulletStruct(_bulletStruct);
                }

                // Update the next fire time
                nextFireTime = Time.time + _fireRate;
            }
        }

        if (context.canceled)
        {
            //Debug.Log("Canceled");
            //Debug.Log(Time.deltaTime);
        }
    }
}
    

