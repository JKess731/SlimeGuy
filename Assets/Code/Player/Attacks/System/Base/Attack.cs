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
    [HideInInspector] public HashSet<GameObject> enemiesToAttack;

    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("player");

        // Testing Code - Remove once Attack System is contained
        transform.parent = player.transform;
        transform.position = player.transform.position;
    }

    public void OnEnable()
    {
        gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
