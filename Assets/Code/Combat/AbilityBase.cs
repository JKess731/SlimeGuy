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

    [Header("Attack Prefab")]
    [SerializeField] protected AttackBehavior behavior;

    [Header("Stats")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _knockback;
    [SerializeField] protected float _range;
    [SerializeField] protected float _speed;

    // Getters
    public float Damage { get => _damage; }
    public float Knockback { get => _knockback; }
    public float Range { get => _range; }
    public float Speed { get => _speed; }

    [Header("Timers")]
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected float _activationTime;

    [HideInInspector] private bool _canActivate = true;
    [HideInInspector] private bool _isActivated = false;

    public bool CanActivate { get => _canActivate; protected set { _canActivate = value; } }
    public bool IsActivated { get => _isActivated; protected set { _isActivated = value; } }
    
    public virtual void Initialize()
    {
        _canActivate = true;
        _isActivated = false;
        behavior.Initialize();
    }

    public virtual void ActivateAbility(InputAction.CallbackContext context, Quaternion rotation, Vector2 attackPosition)
    {
        if (!_canActivate || _isActivated) return;

        throw new System.NotImplementedException();
    }

    #region Cooldown
    public IEnumerator Cooldown()
    {
        _canActivate = false;
        _isActivated = true;

        Debug.Log("Activation Started");

        yield return new WaitForSeconds(_activationTime);
        _isActivated = false;

        Debug.Log("Activation Ended");

        //Debug.Log("Cooldown Started");
        ////Debug.Log("Cooldown Time: " + _cooldownTime);
        //Debug.Log("Can Activate: " + _canActivate);
        //Debug.Log("Is Activated: " + _isActivated);

        yield return new WaitForSeconds(_cooldownTime);
        _canActivate = true;

        Debug.Log("Cooldown Ended");
        //Debug.Log("Can Activate: " + _canActivate);
        //Debug.Log("Is Activated: " + _isActivated);
    }
    #endregion
}
