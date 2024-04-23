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
    // Player Stats
    [field: SerializeField] public float maxHealth { get; private set; }
    [field: SerializeField] public float currentHealth { get; private set; }
    [field: SerializeField] public float attack { get; private set; }
    [field: SerializeField] public float attackSpeed { get; private set; }
    [field: SerializeField] public float defense { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float absorption { get; private set; }
    [field: SerializeField] public float maxAbsorption { get; private set; }
    [field: SerializeField] public float bulletCount { get; private set; }

    // Respawn Variables
    [SerializeField] private Vector2 spawnPos;
    [HideInInspector] public RoomTriggerControl currentRoom;

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

        currentHealth = maxHealth;
        playerStateMachine = GameObject.FindWithTag("player").GetComponent<PlayerStateMachine>();
    }

    //Initializes the health bar && absorption bar
    private void Start()
    {
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
        if (currentHealth + heal >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        //Else heal the player
        currentHealth += heal;
        
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

    #region Stat Increase
    public void IncreaseBulletCount(int amount){bulletCount += amount;}
    public void IncreaseAttack(float amount){attack += amount;}
    public void IncreaseSpeed(float amount) { 
        speed += amount;
        playerStateMachine.setSpeed(speed);
    }
    public void IncreaseAttackSpeed(float amount) { attackSpeed += amount; }
    public void IncreaseDefense(float amount) { defense += amount; }
    public void IncreaseMaxHealth(float amount) { 
        maxHealth += amount; 
        currentHealth += amount; 
        UiManager.instance.UpdateHealthBar(currentHealth, maxHealth);
    }
    public void IncreaseMaxAbsorption(float amount) { maxAbsorption += amount; }
    #endregion
    #region Stat Decrease
    public void DecreaseBulletCount(int amount) { bulletCount -= amount; }
    public void DecreaseAttack(float amount) { attack -= amount; }
    public void DecreaseSpeed(float amount) { 
        speed -= amount;
        playerStateMachine.setSpeed(speed);
    }
    public void DecreaseAttackSpeed(float amount) { attackSpeed -= amount; }
    public void DecreaseDefense(float amount) { defense -= amount; }
    public void DecreaseMaxHealth(float amount) { 
        maxHealth -= amount;
        if (currentHealth > maxHealth)
        {
            currentHealth -= amount;
        }
        UiManager.instance.UpdateHealthBar(currentHealth, maxHealth);
    }
    public void DecreaseMaxAbsorption(float amount) { maxAbsorption -= amount; }
    #endregion
}
