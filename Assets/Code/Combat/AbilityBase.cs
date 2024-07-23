using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbilityBase : ScriptableObject
{
    [Header("Name")]
    [SerializeField] protected string abilityName;

    [Header("Attack Prefab")]
    [SerializeField] protected AttackBehavior attack;

    [Header("Stats")]
    [SerializeField] protected float damage;
    [SerializeField] protected float knockback;
    [SerializeField] protected float range;
    [SerializeField] protected float speed;

    [Header("Timers")]
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float activationTime;
    
    [HideInInspector] protected bool canActivate = true;
    [HideInInspector] protected bool isActivated = false;

    protected virtual void Awake()
    {
       throw new System.NotImplementedException();
    }
    public virtual void Activate(InputAction.CallbackContext context, Quaternion rotation, Vector2 attackPosition)
    {
        Debug.Log(abilityName + ": Activated");
        throw new System.NotImplementedException();
    }

    #region Cooldown
    protected IEnumerator AttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canActivate = true;
    }
    #endregion
}
