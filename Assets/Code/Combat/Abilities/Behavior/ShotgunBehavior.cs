using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Attack/Shotgun")]
public class ShotgunBehavior : Behavior
{
    [Header("Shotgun Attributes")]
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private GameObject _projectile;

    [Header("Prefab Attributes")]
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileKnockback;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRange;

    private BulletStruct _bulletStruct;

    public override void Initialize()
    {
        _bulletStruct = new BulletStruct(_projectileDamage, _projectileKnockback, _projectileSpeed, _projectileRange);
    }

    //Activate the attack
    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            float angleDiff = _spreadAngle * 2 / (_bulletCount - 1);
            for (int i = 0; i < _bulletCount; i++)
            {
                float addedOffset = -angleDiff * i;
                Quaternion newRot = rotation * Quaternion.Euler(0, 0, _spreadAngle) * Quaternion.Euler(0, 0, addedOffset);

                GameObject newBullet = Instantiate(_projectile,attackPosition,newRot);
                newBullet.GetComponent<Bullet>().SetBulletStruct(_bulletStruct);
            }
        }

        if(context.started)
        {
            Debug.Log("Started");
        }
        if (context.performed)
        {
            Debug.Log("Performed");
        }
        if (context.canceled)
        {
            Debug.Log("Canceled");
        }
    }   
}
