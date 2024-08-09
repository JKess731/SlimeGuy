using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    [Header("Ability Variables")]
    [SerializeField] private AbilityBase primary;
    [SerializeField] private AbilityBase secondary;
    [SerializeField] private AbilityBase dash;

    [Header("Attack Position")]
    [SerializeField] private Transform attackPos;

    //Abilities must be initialized, or else they will not work. For some reason,
    //Unity does not read the preassigned values in the Scriptable Objects Variables.
    private void Start()
    {
        try
        {
            primary.Initialize();
            secondary.Initialize();
            dash.Initialize();
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("One or more abilities are not assigned");
        }
    }

    #region Primary
    public void OnPrimary(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }


        if (primary.CanActivate)
        {
            primary.ActivateAbility(context, attackPos.rotation, attackPos.position);
        }

        if (primary.IsActivated)
        {
            StartCoroutine(primary.Cooldown());
        }
    }
    #endregion

    #region Secondary
    public void OnSecondary(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        secondary.ActivateAbility(context, attackPos.rotation, attackPos.position);
        if (secondary.CanActivate && !secondary.IsActivated)
        {
            StartCoroutine(secondary.Cooldown());
        }
    }
    #endregion
}
