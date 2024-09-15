using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "DividingSlash", menuName = "Attack/Dividing Slash")]
public class DividingSlashBehavior : Behavior
{
    [Header("Dividing Slash Attributes")]
    [SerializeField] private GameObject _dividingSlash;
    [SerializeField] private int _slashCount = 1;
    [SerializeField] private float _spreadAngle = 45f;

    [Header("Prefab Attributes")]
    [SerializeField] private int _dividingSlashDamage;
    [SerializeField] private float _dividingSlashKnockback;
    [SerializeField] private float _dividingSlashSpeed;
    [SerializeField] private float _dividingSlashRange;


    private DividingSlashStruct _dividingSlashStruct;

    public override void Initialize()
    {
        _dividingSlashStruct = new DividingSlashStruct(_dividingSlashDamage, _dividingSlashKnockback, _dividingSlashSpeed, _dividingSlashRange);
    }

    //Activate the attack
    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        if (context.started)
        {
            // Calculate the angle step based on the number of slashes and the total spread angle
            float angleStep = _spreadAngle / (_slashCount - 1);
            float currentAngle = -_spreadAngle / 2;  // Start from the negative half of the spread

            Vector2 averageDirection = Vector2.zero;

            for (int i = 0; i < _slashCount; i++)
            {
                // Calculate the new rotation for each slash based on the current angle
                Quaternion newRot = rotation * Quaternion.Euler(0, 0, currentAngle);

                // Instantiate the slash at the calculated position and rotation
                GameObject newDividingSlash = Instantiate(_dividingSlash, attackPosition, newRot);
                newDividingSlash.GetComponent<DividingSlash>().SetDividingSlashStruct(_dividingSlashStruct);

                Vector2 slashDirection = newRot * Vector2.right;  // Right direction of the slash
                averageDirection += slashDirection;

                // Increment the angle for the next slash
                currentAngle += angleStep;
            }
        }
    }
}