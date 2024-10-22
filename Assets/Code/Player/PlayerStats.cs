using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Created by:
 * Last Edited by: Jared Kessler
 * Modified on: April 18th, 2024 (Edison Li)
 */

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public PlayerStateMachine playerStateMachine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("PlayerStats instance already exists. Destroying duplicate.");
            Destroy(gameObject);
        }
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Start()
    {
    }
}
