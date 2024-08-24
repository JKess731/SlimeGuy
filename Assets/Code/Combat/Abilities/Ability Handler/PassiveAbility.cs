using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Secondary", menuName = "Ability/Primary Ability")]
public class PassiveAbility : AbilityBase
{
    public override void ActivateAbility()
    {
        behavior.Activate();
    }
}
