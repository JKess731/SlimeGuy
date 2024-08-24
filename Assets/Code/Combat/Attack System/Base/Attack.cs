using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack: MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float activationTime;

    [Header("Status Effects")]
    [SerializeField] protected Effect[] baseEffects;
    List<Effect> currentEffects;


    [SerializeField] protected List<Effect> effects = new List<Effect>();

    [HideInInspector] protected bool canActivate = true;
    [HideInInspector] protected bool isActivated = false;

    protected GameObject player;
    [HideInInspector] public HashSet<GameObject> enemiesToAttack = new HashSet<GameObject>();
   

    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("player");

        // Testing Code - Remove once Attack System is more contained and managed
        transform.parent = player.transform;
        transform.position = player.transform.position;
    }

    #region Enable/Disable

    public virtual void OnEnable()
    {
        gameObject.SetActive(true);
        canActivate = true;
    }

    public void OnDisable()
    {
        gameObject.SetActive(false);
        canActivate = false;
        isActivated = false;
    }

    #endregion

    #region Cooldown
    protected IEnumerator AttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canActivate = true;
    }
    #endregion

    #region Status Effects

    public List<Effect> GetEffects()
    {
        return effects;
    }
    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
    }
    public void RemoveEffect(Effect effect)
    {
        if (effects.Contains(effect))
        {
            effects.Remove(effect);
        }
    }

    public void ApplyEffects(GameObject target)
    {
        foreach (Effect effect in effects)
        {
            effect.ApplyModifier(target);
        }
    }

    #endregion

}
