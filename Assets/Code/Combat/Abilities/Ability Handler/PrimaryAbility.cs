using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Primary", menuName = "Ability/Primary Ability")]
public class PrimaryAbility : AbilityBase
{
    public override void ActivateAbility(InputAction.CallbackContext context, Quaternion rotation, Vector2 position2d)
    {
        behavior.Activate(context, position2d, rotation);
        base.AbilityState = AbilityState.Active;
    }
}