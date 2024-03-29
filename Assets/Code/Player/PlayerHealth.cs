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
    [SerializeField] GameObject player;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Vector2 spawnPos;

    [SerializeField] private PlayerHealthbar healthBar;
    

    [HideInInspector] public RoomTriggerControl currentRoom;

    private AnimationControl playerAnimationControl;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<PlayerHealthbar>();

        currentHealth = maxHealth;

        spawnPos = transform.position;

        playerAnimationControl = GetComponent<AnimationControl>();
    }


    //Handles Damage
    public void Damage(int damage)
    {
        Debug.Log("Taking Damage: " + currentHealth);
        currentHealth -= damage;
        healthBar.setHealth((float) currentHealth / maxHealth);

        //playerAnimationControl.printStates();

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
        healthBar.setHealth(currentHealth);
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
