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
    [SerializeField] private bool whiteHitAnimation;

    [HideInInspector] public RoomTriggerControl currentRoom;

    private AnimationControl playerAnimationControl;
    private PlayerMove playerMovement;

    private void Awake()
    {
        currentHealth = maxHealth;
        spawnPos = transform.position;

        playerAnimationControl = GetComponent<AnimationControl>();
        playerMovement = GetComponent<PlayerMove>();
    }


    //Handles Damage
    public void Damage(int damage)
    {
        Debug.Log("Taking Damage: " + currentHealth);
        currentHealth -= damage;

        playerAnimationControl.isBeingHit = true;

        if (whiteHitAnimation)
        {
            if (playerMovement.directionX > 0)
            {
                playerAnimationControl.currentState = AnimState.HIT_RIGHT_WHITE;
            }
            else
            {
                playerAnimationControl.currentState = AnimState.HIT_LEFT_WHITE;
            }
        }
        else
        {
            if (playerMovement.directionX > 0)
            {
                playerAnimationControl.currentState = AnimState.HIT_RIGHT_RED;
            }
            else
            {
                playerAnimationControl.currentState = AnimState.HIT_LEFT_RED;
            }
        }

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

            playerAnimationControl.currentState = AnimState.IDLE_LEFT;

            // Re-activate triggers in the room
            // ONE ROOM TESTING ONLY
            currentRoom.triggerParentGameObject.SetActive(true);

            Debug.Log("You Lose");
        }
    }
}
