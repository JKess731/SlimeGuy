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
    

    [HideInInspector] public RoomTriggerControl currentRoom;

    private AnimationControl playerAnimationControl;

    private void Awake()
    {
        currentHealth = maxHealth;
        spawnPos = transform.position;

        playerAnimationControl = GetComponent<AnimationControl>();
    }


    //Handles Damage
    public void Damage(int damage)
    {
        Debug.Log("Taking Damage: " + currentHealth);
        currentHealth -= damage;

        playerAnimationControl.isBeingHit = true;

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
}
