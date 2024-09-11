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
    [SerializeField] private PassiveAbility passive;

    [Header("Attack Position")]
    [SerializeField] private Transform attackPos;

    //Abilities must be initialized, or else they will not work. For some reason,
    //Unity does not read the preassigned values in the Scriptable Objects Variables.

    public AbilityBase Primary { get => primary; }
    public AbilityBase Secondary { get => secondary; }
    public AbilityBase Dash { get => dash; }
    public PassiveAbility Passive { get => passive; }
    private void Start()
    {
        try
        {
            primary.Initialize();
            secondary.Initialize();
            dash.Initialize();
            passive.Initialize();
        }
        catch (NullReferenceException)
        {
            Debug.LogWarning("One or more abilities are not assigned");
        }
    }

    #region Primary
    public void OnPrimary(InputAction.CallbackContext context)
    {
        if (primary.Behavior.AbilityState == AbilityState.READY)
        {
            primary.ActivateAbility(context, attackPos.rotation, attackPos.position);
        }

        //if (primary.Behavior.AbilityState == AbilityState.FINISHED)
        //{
        //    Debug.Log("Start Cooldown");
        //    StartCoroutine(primary.Cooldown());
        //}
    }
    public void OnPrimaryStarted(InputAction.CallbackContext context)
    {
        primary.Behavior.StartBehavior(attackPos.position, attackPos.rotation);
    }
    public void OnPrimaryPerformed(InputAction.CallbackContext context)
    {
        primary.Behavior.PerformBehavior(attackPos.position, attackPos.rotation);
    }
    public void OnPrimaryCanceled(InputAction.CallbackContext context)
    {
        primary.Behavior.CancelBehavior(attackPos.position, attackPos.rotation);
    }
    #endregion

    #region Secondary
    public void OnSecondary(InputAction.CallbackContext context)
    {
        if (secondary.Behavior.AbilityState == AbilityState.READY)
        {
            secondary.ActivateAbility(context, attackPos.rotation, attackPos.position);
        }

        if (secondary.Behavior.AbilityState == AbilityState.FINISHED)
        {
            StartCoroutine(secondary.Cooldown());
        }
    }
    #endregion

    #region Dash
    public void OnDash(InputAction.CallbackContext context)
    {
        if (dash.Behavior.AbilityState == AbilityState.READY)
        {
            dash.ActivateAbility(context, attackPos.rotation, attackPos.position);
        }

        if (dash.Behavior.AbilityState == AbilityState.FINISHED)
        {
            StartCoroutine(dash.Cooldown());
        }
    }
    #endregion 

    #region Passive
    public void OnPassive()
    {
        if (passive.Behavior.AbilityState == AbilityState.READY)
        {
            passive.ActivateAbility();
        }

        if (passive.Behavior.AbilityState == AbilityState.FINISHED)
        {
            StartCoroutine(passive.Cooldown());
        }
    }
    #endregion
}