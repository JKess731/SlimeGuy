using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class AbilityManager : MonoBehaviour
{
    [Header("Ability Variables")]
    [SerializeField] private AbilityBaseSO primary;
    [SerializeField] private AbilityBaseSO secondary;
    [SerializeField] private AbilityBaseSO dash;
    [SerializeField] private AbilityBaseSO passive;

    [Header("Attack Position")]
    [SerializeField] private Transform attackPos;

    //Abilities must be initialized, or else they will not work. For some reason,
    //Unity does not read the preassigned values in the Scriptable Objects Variables.

    public AbilityBaseSO Primary { get => primary; }
    public AbilityBaseSO Secondary { get => secondary; }
    public AbilityBaseSO Dash { get => dash; }
    public AbilityBaseSO Passive { get => passive; }

    private void Awake()
    {

        if (primary != null)
        {
            primary = Instantiate(primary);
        }
        if (secondary != null)
        {
            secondary = Instantiate(secondary);
        }
        if (dash != null)
        {
            dash = Instantiate(dash);
        }

        primary?.Initialize(this);
        secondary?.Initialize(this);
        dash?.Initialize(this);

        //primary = Instantiate(primary);
        try
        {
            primary.onBehaviorFinished += OnPrimaryCooldown;
            secondary.onBehaviorFinished += OnSecondaryCooldown;
            dash.onBehaviorFinished += OnDashCooldown;
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning("One Behavior not Found, reload ability into slot");
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
    public void InstaniatePrimary(AbilityBaseSO newAbilitySO)
    {
        if (primary != null)
        {
            primary.onBehaviorFinished -= OnPrimaryCooldown;
        }

        //primary = Instantiate(newAbilitySO);
        primary?.Initialize(this);
        primary.onBehaviorFinished += OnPrimaryCooldown;
    }
    public void OnPrimaryStarted(InputAction.CallbackContext context)
    {
        if(primary.AbilityState == AbilityState.READY)
        {
            primary?.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnPrimaryPerformed(InputAction.CallbackContext context)
    {
        if (primary?.AbilityState == AbilityState.PERFORMING)
        {
            primary?.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnPrimaryCanceled(InputAction.CallbackContext context)
    {
        if (primary?.AbilityState == AbilityState.CANCELING)
        {
            primary?.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnPrimaryCooldown()
    {
        //UiManager.instance?.TextAndSliderAdjustment(primary, "P");
        StartCoroutine(primary.Cooldown());
    }
    #endregion

    #region Secondary
    public void InstaniateSecondary(AbilityBaseSO newAbilitySO)
    {
        if (secondary != null)
        {
            secondary.onBehaviorFinished -= OnSecondaryCooldown;
        }

        secondary = Instantiate(newAbilitySO);
        secondary?.Initialize(this);
        secondary.onBehaviorFinished += OnSecondaryCooldown;

        UiManager.instance?.UpdateSecondaryAbilityImage(secondary.Icon);
    }
    public void OnSecondaryStarted(InputAction.CallbackContext context)
    {
        if (secondary.AbilityState == AbilityState.READY)
        {
            secondary?.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryPerformed(InputAction.CallbackContext context)
    {
        if (secondary.AbilityState == AbilityState.STARTING)
        {
            secondary?.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryCanceled(InputAction.CallbackContext context)
    {
        if(secondary.AbilityState == AbilityState.PERFORMING)
        {
            secondary?.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryCooldown()
    {
        UiManager.instance?.TextAndSliderAdjustment(secondary, "S");
        StartCoroutine(secondary.Cooldown());
    }
    #endregion

    #region Dash
    public void InstaniateDash()
    {
        dash.onBehaviorFinished -= OnDashCooldown;

        dash = Instantiate(dash);
        dash?.Initialize(this);
        dash.onBehaviorFinished += OnDashCooldown;

        UiManager.instance?.UpdateDashAbilityImage(dash.Icon);
    }
    public void OnDashStarted(InputAction.CallbackContext context)
    {
        if (dash.AbilityState == AbilityState.READY)
        {
            dash?.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (dash.AbilityState == AbilityState.STARTING)
        {
            dash?.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashCanceled(InputAction.CallbackContext context)
    {
        if (dash.AbilityState == AbilityState.PERFORMING)
        {
            dash?.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashCooldown()
    {
        UiManager.instance?.TextAndSliderAdjustment(dash, "D");
        StartCoroutine(dash .Cooldown());
    }
    #endregion 

    #region Passive
    public void OnPassive()
    {
        Debug.Log("Passive");
    }
    #endregion

    public void CallCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    public void UpgradeAbilities(StatsSO playerStats, StatsEnum statType)
    {
        primary?.Upgrade(playerStats, statType);
        secondary?.Upgrade(playerStats, statType);
        dash?.Upgrade(playerStats, statType);
    }
}