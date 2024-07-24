using System.Collections;
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
    [SerializeField] protected float damage;
    [SerializeField] protected float knockback;
    [SerializeField] protected float range;
    [SerializeField] protected float speed;

    [Header("Timers")]
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float activationTime;
    
    [HideInInspector] protected bool canActivate = true;
    [HideInInspector] protected bool isActivated = false;

    public virtual void ActivateAbility(InputAction.CallbackContext context, Quaternion rotation, Vector2 attackPosition)
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

    public void test()
    {
        AttackCooldown(5f);
    }
}
