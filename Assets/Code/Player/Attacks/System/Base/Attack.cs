using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack: MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float activationTime;

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

    public void OnEnable()
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

    protected IEnumerator AttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canActivate = true;
    }
}
