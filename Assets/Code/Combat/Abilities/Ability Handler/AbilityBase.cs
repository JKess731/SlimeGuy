using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The SO base class for all abilities
/// </summary>
public abstract class AbilityBase : ScriptableObject
{
    [Header("Name")]
    [SerializeField] protected string abilityName;
    public string AbilityName { get => abilityName; }

    [Header("Icon")]
    [SerializeField] protected Sprite icon;
    public Sprite Icon { get => icon; }

    [Header("Behavior SO")]
    [SerializeField] protected Behavior behavior;

    [HideInInspector] protected AbilityState _abilityState = AbilityState.Ready;
    public AbilityState AbilityState { get => _abilityState; protected set => _abilityState = value; }
    
    /// <summary>
    /// Initializes the ability
    /// Used to set up the base state of the ability
    /// </summary>
    public virtual void Initialize()
    {
        _abilityState = AbilityState.Ready;
        behavior.Initialize();
    }

    /// <summary>
    /// Activation function for abilities
    /// </summary>
    /// <param name="context"> The context of the player inputs </param>
    /// <param name="rotation"> The desired quanternion for attack prefabs </param>
    /// <param name="attackPosition"> The 2d position that the attack prefabs are instanitated </param>
    /// <exception cref="System.NotImplementedException"></exception>
    public virtual void ActivateAbility(InputAction.CallbackContext context, Quaternion rotation, Vector2 attackPosition)
    {
        throw new System.NotImplementedException();
    }

    public virtual void ActivateAbility()
    {
        throw new System.NotImplementedException();
    }

    #region Cooldown
    /// <summary>
    /// A coroutine that handles the cooldown of the ability
    /// </summary>
    /// <returns>The current wait for seconds left</returns>
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(behavior.ActivationTime);

        _abilityState = AbilityState.Deactive;
        //Debug.Log("Activation Ended");
        yield return new WaitForSeconds(behavior.CooldownTime);

        _abilityState = AbilityState.Ready;
        //Debug.Log("Cooldown Ended");
    }
    #endregion
}

public enum AbilityState
{
    Ready,
    Active,
    Deactive
}
