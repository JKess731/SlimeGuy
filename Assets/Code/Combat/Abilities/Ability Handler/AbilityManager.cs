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
        if (primary.AbilityState == AbilityState.Ready)
        {
            primary.ActivateAbility(context, attackPos.rotation, attackPos.position);
        }

        if (primary.AbilityState == AbilityState.Active && context.started)
        {
            StartCoroutine(primary.Cooldown());
            try
            {
                StartCoroutine(UiManager.instance.TextAndSliderAdjustment(primary, "P"));
            }
            catch (NullReferenceException)
            {
                Debug.LogWarning("UI Manager is not assigned");
            }
            //StartCoroutine(UiManager.instance.TextAndSliderAdjustment(primary, "P"));
            Debug.Log("I got to the end of OnPrimary");
        }
    }
    #endregion

    #region Secondary
    public void OnSecondary(InputAction.CallbackContext context)
    {
        if (secondary.AbilityState == AbilityState.Ready)
        {
            secondary.ActivateAbility(context, attackPos.rotation, attackPos.position);
        }

        if (secondary.AbilityState == AbilityState.Active && context.started)
        {
            StartCoroutine(secondary.Cooldown());

            try
            {
                StartCoroutine(UiManager.instance.TextAndSliderAdjustment(secondary, "S"));
            }
            catch (NullReferenceException)
            {
                Debug.LogWarning("UI Manager is not assigned");
            }
            //StartCoroutine(UiManager.instance.TextAndSliderAdjustment(secondary, "S"));
            Debug.Log("I got to the end of OnSecondary");
        }
    }
    #endregion

    #region Dash
    public void OnDash(InputAction.CallbackContext context)
    {
        if (dash.AbilityState == AbilityState.Ready)
        {
            dash.ActivateAbility(context, attackPos.rotation, attackPos.position);
        }

        if (dash.AbilityState == AbilityState.Ready && context.started)
        {
            StartCoroutine(dash.Cooldown());
            StartCoroutine(UiManager.instance.TextAndSliderAdjustment(dash, "D"));
            Debug.Log("I got to the end of OnDash");
        }
    }
    #endregion 

    #region Passive
    public void OnPassive()
    {
        if (passive.AbilityState == AbilityState.Ready)
        {
            passive.ActivateAbility();
        }

        if (passive.AbilityState == AbilityState.Active)
        {
            StartCoroutine(passive.Cooldown());
            StartCoroutine(UiManager.instance.TextAndSliderAdjustment(passive, "PA"));
        }
    }
    #endregion
}