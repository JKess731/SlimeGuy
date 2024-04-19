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
    [SerializeField] public int maxHealth { get; private set; } = 10; 
    [SerializeField] public int currentHealth { get; private set; } = 10;
    [SerializeField] public int attack { get; private set; } = 10;
    [SerializeField] public int defense { get; private set; } = 10;
    [SerializeField] public int speed { get; private set; } = 10;
    [SerializeField] public int absorption { get; private set; } = 0;
    [SerializeField] public int maxAbsorption { get; private set; } = 10;


    // Respawn Variables
    [SerializeField] private Vector2 spawnPos;
    [HideInInspector] public RoomTriggerControl currentRoom;

    [SerializeField] private UIManager uiManager;

    public static PlayerStats instance;

    private void Awake()
    {
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();

        currentHealth = maxHealth;

        spawnPos = transform.position;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("PlayerStats instance already exists. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    //Initializes the health bar && absorption bar
    private void Start()
    {
        uiManager.UpdateHealthBar(currentHealth, maxHealth);
        uiManager.UpdateAbsorptionBar(absorption, maxAbsorption);
    }

    //Handles Damage
    public void Damage(int damage)
    {
        currentHealth -= damage;
        uiManager.UpdateHealthBar(currentHealth, maxHealth);

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
        uiManager.UpdateHealthBar(currentHealth, maxHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void InreaseAbsorption(int amount)
    {
        if (absorption + amount <= maxAbsorption)
        {
            absorption += amount;
            uiManager.UpdateAbsorptionBar(absorption, maxAbsorption);
        }
    }
}
