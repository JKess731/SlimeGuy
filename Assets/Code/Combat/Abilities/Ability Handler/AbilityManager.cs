using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    [SerializeField] private AbilityMonoBase dash;
    [SerializeField] private AbilityMonoBase [] secondary = new AbilityMonoBase[2];     //Secondary has initially two abilities
    [SerializeField] private AbilityMonoBase[] passive;

    [Header("Attack Position")]
    [SerializeField] private Transform attackPos;

    private Transform primaryHolder;
    private Transform secondaryHolder;
    private Transform dashHolder;
    private Transform passiveHolder;

    public static AbilityManager Instance;

    //Abilities must be initialized, or else they will not work. For some reason,
    //Unity does not read the preassigned values in the Scriptable Objects Variables.

    public AbilityMonoBase Primary { get => primary; }
    public AbilityMonoBase Secondary { get => secondary[0]; }
    public AbilityMonoBase Secondary2 { get => secondary[1]; }
    public AbilityMonoBase Dash { get => dash; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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

        attackPos = GameObject.Find("Ring").transform.GetChild(0).transform;
    }

    private void Start()
    {
        //Initialize all abilities
        //Hover over the Initialize method to see what it does
        primary?.Initialize();

        for (int i = 0; i < secondary.Length; i++)
        {
            secondary[i]?.Initialize();
        }
        dash?.Initialize();


        foreach (AbilityMonoBase ability in passive)
        {
            ability?.Initialize();
        }

        UiManager.instance.UpdateAllIcons();
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
                int index = secondary.Length;
                if(index > secondary.Length)
                {
                    Debug.LogError("Index out of range");
                    return;
                }

                secondary[index]?.gameObject.SetActive(false);
                secondary[index] = secondaryDict[abilityName];
                secondary[index]?.Initialize();
                break;

            case AbilityType.DASH:
                dash?.gameObject.SetActive(false);
                dash = dashDict[abilityName];
                dash?.Initialize();
                break;
        }
    }

    //Used to subscribe to the primary input
    #region Primary
    public void OnPrimaryStarted(InputAction.CallbackContext context)
    {
        if(primary?.AbilityState == AbilityState.READY)
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

    //Used to subscribe to the secondary input 
    #region All Secondary

    #region Secondary1
    public void OnSecondaryStarted(InputAction.CallbackContext context)
    {
        if (secondary[0]?.AbilityState == AbilityState.READY)
        {
            secondary[0]?.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryPerformed(InputAction.CallbackContext context)
    {
        if (secondary[0]?.AbilityState == AbilityState.STARTING)
        {
            secondary[0]?.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondaryCanceled(InputAction.CallbackContext context)
    {
        if(secondary[0]?.AbilityState == AbilityState.PERFORMING)
        {
            secondary[0]?.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    #endregion

    #region Secondary2
    public void OnSecondary2Started(InputAction.CallbackContext context)
    {
        if (secondary[1]?.AbilityState == AbilityState.READY)
        {
            secondary[1]?.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondary2Performed(InputAction.CallbackContext context)
    {
        if (secondary[1]?.AbilityState == AbilityState.STARTING)
        {
            secondary[1]?.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnSecondary2Canceled(InputAction.CallbackContext context)
    {
        if (secondary[1]?.AbilityState == AbilityState.PERFORMING)
        {
            secondary[1]?.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }
    #endregion

    #endregion

    //Used to subscribe to the dash input 
    #region Dash
    public void OnDashStarted(InputAction.CallbackContext context)
    {
        if (dash?.AbilityState == AbilityState.READY)
        {
            dash?.StartBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (dash?.AbilityState == AbilityState.STARTING)
        {
            dash?.PerformBehavior(attackPos.position, attackPos.rotation);
        }
    }
    public void OnDashCanceled(InputAction.CallbackContext context)
    {
        if (dash?.AbilityState == AbilityState.PERFORMING)
        {
            dash?.CancelBehavior(attackPos.position, attackPos.rotation);
        }
    }

    #endregion 

    //Used to subscribe to the passive
    #region Passive
    public void OnPassive()
    {
        //Debug.Log("Passive");
    }
    #endregion


    public string AbilityUIType(AbilityMonoBase ability)
    {
        if(primary?.GetType() == ability.GetType())
        {
            return "P";
        }
        else if(secondary?.GetType() == ability.GetType())
        {
            return "S";
        }
        else if(dash?.GetType() == ability.GetType())
        {
            return "D";
        }
        else if(passive?.GetType() == ability.GetType())
        {
            return "PA";
        }
        return "WRONG";
    }
}