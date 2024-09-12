using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
    public Behavior Behavior { get => behavior; }
    
    /// <summary>
    /// Initializes the ability
    /// Used to set up the base state of the ability
    /// </summary>
    public virtual void Initialize()
    {
        behavior?.Initialize(this);
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
}
