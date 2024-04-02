using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Created by:
 * 
 * Last Edited by: Jared Kessler
 */

public class PlayerHealth : MonoBehaviour
{
    // Health Variables
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private UIManager uiManager;

    // Respawn Variables
    [SerializeField] private Vector2 spawnPos;

    [HideInInspector] public RoomTriggerControl currentRoom;

    private void Awake()
    {
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();

        currentHealth = maxHealth;

        spawnPos = transform.position;
    }

    //Initializes the health bar && absorption bar
    private void Start()
    {
        uiManager.UpdateHealthBar(currentHealth, maxHealth);
        uiManager.UpdateAbsorptionBar(currentHealth, maxHealth);
    }

    //Handles Damage
    public void Damage(int damage)
    {
        currentHealth -= damage;
        uiManager.UpdateHealthBar(currentHealth, maxHealth);
        uiManager.UpdateAbsorptionBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            transform.position = spawnPos;
            currentHealth = maxHealth;

            foreach (GameObject enemy in currentRoom.spawnedEnemies)
            {
                Destroy(enemy);
            }

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
}
