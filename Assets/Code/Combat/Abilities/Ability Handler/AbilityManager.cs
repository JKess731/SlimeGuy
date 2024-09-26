using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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

    private void Start()
    {
        primary = Instantiate(primary);
        secondary = Instantiate(secondary);
        dash = Instantiate(dash);

        primary?.Initialize();
        secondary?.Initialize();
        dash?.Initialize();

        try
        {
            primary.Behavior.onBehaviorFinished += OnPrimaryCooldown;
            secondary.Behavior.onBehaviorFinished += OnSecondaryCooldown;
            dash.Behavior.onBehaviorFinished += OnDashCooldown;
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning("One Behavior not Found, reload ability into slot");
        }
    }

    private void OnDestroy()
    {
        if (primary != null)
        {
            primary.Behavior.onBehaviorFinished -= OnPrimaryCooldown;
        }

        if (secondary != null)
        {
            secondary.Behavior.onBehaviorFinished -= OnSecondaryCooldown;
        }

        if (dash != null)
        {
            dash.Behavior.onBehaviorFinished -= OnDashCooldown;
        }
    }

    //private void Update()
    //{
    //    if (primary.Behavior.AbilityState == AbilityState.FINISHED)
    //    {
    //        StartCoroutine(primary.Behavior.Cooldown());
    //    }
    //    if (secondary.Behavior.AbilityState == AbilityState.FINISHED)
    //    {
    //        StartCoroutine(secondary.Behavior.Cooldown());
    //    }
    //    if (dash.Behavior.AbilityState == AbilityState.FINISHED)
    //    {
    //        StartCoroutine(dash.Behavior.Cooldown());
    //    }
    //}

    #region Primary
    public void InstaniatePrimary(AbilityBase newAbility)
    {
        if (primary != null)
        {
            primary.Behavior.onBehaviorFinished -= OnPrimaryCooldown;
        }

        primary = Instantiate(newAbility);
        primary?.Initialize();
        primary.Behavior.onBehaviorFinished += OnPrimaryCooldown;
    }

    public void OnPrimaryStarted(InputAction.CallbackContext context)
    {
        if(primary.Behavior.AbilityState == AbilityState.READY)
        {
            primary?.Behavior.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnPrimaryPerformed(InputAction.CallbackContext context)
    {
        if(primary.Behavior.AbilityState == AbilityState.PERFORMING)
        {
            primary?.Behavior.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnPrimaryCanceled(InputAction.CallbackContext context)
    {
        if (primary.Behavior.AbilityState == AbilityState.CANCELING)
        {
            primary?.Behavior.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }

    public void OnPrimaryCooldown()
    {
        StartCoroutine(primary.Behavior.Cooldown());
    }
    #endregion

    #region Secondary

    public void InstaniateSecondary(AbilityBase newAbility)
    {
        if (secondary != null)
        {
            secondary.Behavior.onBehaviorFinished -= OnSecondaryCooldown;
        }

        secondary = Instantiate(newAbility);
        secondary?.Initialize();
        secondary.Behavior.onBehaviorFinished += OnSecondaryCooldown;
    }

    public void OnSecondaryStarted(InputAction.CallbackContext context)
    {
        if (secondary.Behavior.AbilityState == AbilityState.READY)
        {
            secondary?.Behavior.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryPerformed(InputAction.CallbackContext context)
    {
        if (secondary.Behavior.AbilityState == AbilityState.STARTING)
        {
            secondary?.Behavior.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryCanceled(InputAction.CallbackContext context)
    {
        if(secondary.Behavior.AbilityState == AbilityState.PERFORMING)
        {
            secondary?.Behavior.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryCooldown()
    {
        StartCoroutine(secondary.Behavior.Cooldown());
    }
    #endregion

    #region Dash
    public void InstaniateDash()
    {
        dash.Behavior.onBehaviorFinished -= OnDashCooldown;

        dash = Instantiate(dash);
        dash?.Initialize();
        dash.Behavior.onBehaviorFinished += OnDashCooldown;
    }
    public void OnDashStarted(InputAction.CallbackContext context)
    {
        if (dash.Behavior.AbilityState == AbilityState.READY)
        {
            dash?.Behavior.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (dash.Behavior.AbilityState == AbilityState.STARTING)
        {
            dash?.Behavior.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashCanceled(InputAction.CallbackContext context)
    {
        if (dash.Behavior.AbilityState == AbilityState.PERFORMING)
        {
            dash?.Behavior.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashCooldown()
    {
        StartCoroutine(dash.Behavior.Cooldown());
    }
    #endregion 

    #region Passive
    public void OnPassive()
    {
        Debug.Log("Passive");
    }
    #endregion
}