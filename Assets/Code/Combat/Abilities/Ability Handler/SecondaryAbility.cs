using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Secondary", menuName = "Ability/Secondary Ability")]
public class SecondaryAbility : AbilityBase
{
    public override void ActivateAbility(InputAction.CallbackContext context, Quaternion rotation, Vector2 position2d)
    {
        behavior.Activate(context, position2d, rotation);
    }
}
