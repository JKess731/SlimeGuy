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
    [SerializeField] private StatsSO playerStats;

    // Player Stats
    [field: SerializeField] public float maxHealth { get; private set; } = 0;
    [field: SerializeField] public float currentHealth { get; private set; } = 0;
    [field: SerializeField] public float attack { get; private set; } = 0;
    [field: SerializeField] public float attackSpeed { get; private set; } = 0;
    [field: SerializeField] public float defense { get; private set; } = 0;
    [field: SerializeField] public float speed { get; private set; } = 0;
    [field: SerializeField] public float absorption { get; private set; } = 0;
    [field: SerializeField] public float maxAbsorption { get; private set; } = 0;
    [field: SerializeField] public float bulletCount { get; private set; } = 0;

    // Respawn Variables
    [SerializeField] private Vector2 spawnPos;
    [HideInInspector] public RoomTriggerControl currentRoom;

    public static PlayerStats instance;

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
        playerStats.Initialize();
        spawnPos = transform.position;

        maxHealth = playerStats.GetStat(Enum_Stat.HEALTH);
        currentHealth = maxHealth;
        attack = playerStats.GetStat(Enum_Stat.ATTACK);
        attackSpeed = playerStats.GetStat(Enum_Stat.ATTACKSPEED);
        defense = playerStats.GetStat(Enum_Stat.DEFENSE);
        speed = playerStats.GetStat(Enum_Stat.SPEED);
        maxAbsorption = playerStats.GetStat(Enum_Stat.ABSORPTION);
        bulletCount = playerStats.GetStat(Enum_Stat.BULLETCOUNT);
    }

    //Initializes the health bar && absorption bar
    private void Start()
    {
        print();
        UiManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        UiManager.instance.UpdateAbsorptionBar(absorption, maxAbsorption);
    }

    //Handles Damage
    public void Damage(int damage)
    {
        currentHealth -= damage;
        UiManager.instance.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            transform.position = spawnPos;
            currentHealth = maxHealth;

            //foreach (GameObject enemy in currentRoom.spawnedEnemies)
            //{
            //    Destroy(enemy);
            //}

            // Clear the list
            currentRoom.spawnedEnemies.Clear();

            // Re-activate triggers in the room
            // ONE ROOM TESTING ONLY
            currentRoom.triggerParentGameObject.SetActive(true);

            Debug.Log("You Lose");
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UiManager.instance.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void IncreaseAbsorption(int amount)
    {
        if (absorption + amount <= maxAbsorption)
        {
            absorption += amount;
            UiManager.instance.UpdateAbsorptionBar(absorption, maxAbsorption);
        }
    }
    public void print()
    {
        Debug.Log("Health: " + currentHealth);
        Debug.Log("Attack: " + attack);
        Debug.Log("Attack Speed: " + attackSpeed);
        Debug.Log("Defense: " + defense);
        Debug.Log("Speed: " + speed);
        Debug.Log("Absorption: " + absorption);
        Debug.Log("Bullet Count: " + bulletCount);
    }
}
