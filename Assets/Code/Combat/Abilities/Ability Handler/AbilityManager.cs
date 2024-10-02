using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class AbilityManager : MonoBehaviour
{

    [Header("Ability Dictionaries")]
    [HideInInspector]
    [SerializeField] private Dictionary<string, AbilityMonoBase> primaryDict = new Dictionary<string, AbilityMonoBase >();
    [SerializeField] private Dictionary<string, AbilityMonoBase> secondaryDict = new Dictionary<string, AbilityMonoBase>();
    [SerializeField] private Dictionary<string, AbilityMonoBase> dashDict = new Dictionary<string, AbilityMonoBase>();
    [SerializeField] private Dictionary<string, AbilityMonoBase> passiveDict = new Dictionary<string, AbilityMonoBase>();

    [Header("Ability Variables")]
    [SerializeField] private AbilityMonoBase primary;
    [SerializeField] private AbilityMonoBase secondary;
    [SerializeField] private AbilityMonoBase dash;
    [SerializeField] private AbilityMonoBase[] passive;

    [Header("Attack Position")]
    [SerializeField] private Transform attackPos;

    private Transform primaryHolder;
    private Transform secondaryHolder;
    private Transform dashHolder;
    private Transform passiveHolder;

    //Abilities must be initialized, or else they will not work. For some reason,
    //Unity does not read the preassigned values in the Scriptable Objects Variables.

    public AbilityMonoBase Primary { get => primary; }
    public AbilityMonoBase Secondary { get => secondary; }
    public AbilityMonoBase Dash { get => dash; }

    private void Awake()
    {
        try
        {
            primaryHolder = GameObject.Find("Primary Holder").transform;
            secondaryHolder = GameObject.Find("Secondary Holder").transform;
            dashHolder = GameObject.Find("Dash Holder").transform;
            passiveHolder = GameObject.Find("Passive Holder").transform;
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("One or more Ability Holders are missing. Please make sure that the Ability Holders are named correctly.");
            Debug.LogError(e);
        }


        for (int i = 0; i < primaryHolder.childCount; i++)
        {
            if(primaryHolder.GetChild(i).TryGetComponent(out AbilityMonoBase child))
            {
                primaryDict.Add(child.AbilityName, child);
            }
        }

        for (int i = 0; i < secondaryHolder.childCount; i++)
        {
            if(secondaryHolder.GetChild(i).TryGetComponent(out AbilityMonoBase child))
            {
                secondaryDict.Add(child.AbilityName, child);
            }
        }

        for (int i = 0; i < dashHolder.childCount; i++)
        {
            if(dashHolder.GetChild(i).TryGetComponent(out AbilityMonoBase child))
            {
                dashDict.Add(child.AbilityName, child);
            }
        }

        for (int i = 0; i < passiveHolder.childCount; i++)
        {
            if(passiveHolder.GetChild(i).TryGetComponent(out AbilityMonoBase child))
            {
                passiveDict.Add(child.AbilityName, child);
            }
        }

        Swap(AbilityType.PRIMARY, "Shotgun Slime Pellet");
        Swap(AbilityType.PRIMARY, "DividingSlash");
        Swap(AbilityType.SECONDARY, "SlimeWhip");
    }

    public void Swap(AbilityType abilityType, string abilityName)
    {
        switch (abilityType)
        {
            case AbilityType.PRIMARY:
                primary?.gameObject.SetActive(false);
                primary = primaryDict[abilityName];
                primary?.Initialize();
                break;

            case AbilityType.SECONDARY:
                secondary?.gameObject.SetActive(false);
                secondary = secondaryDict[abilityName];
                secondary?.Initialize();
                break;

            case AbilityType.DASH:
                dash?.gameObject.SetActive(false);
                dash = dashDict[abilityName];
                dash?.Initialize();
                break;
        }
    }

    #region Primary
    public void OnPrimaryStarted(InputAction.CallbackContext context)
    {
        if(primary.AbilityState == AbilityState.READY)
        {
            primary?.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnPrimaryPerformed(InputAction.CallbackContext context)
    {
        if (primary?.AbilityState == AbilityState.STARTING)
        {
            primary?.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnPrimaryCanceled(InputAction.CallbackContext context)
    {
        if (primary.AbilityState == AbilityState.PERFORMING)
        {
            primary?.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    #endregion

    #region Secondary
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
    #endregion

    #region Dash
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

    #endregion 

    #region Passive
    public void OnPassive()
    {
        //Debug.Log("Passive");
    }
    #endregion

    public void UpgradeAbilities(StatsSO playerStats, StatsEnum statType)
    {
        primary?.Upgrade(playerStats, statType);
        secondary?.Upgrade(playerStats, statType);
        dash?.Upgrade(playerStats, statType);
    }
}