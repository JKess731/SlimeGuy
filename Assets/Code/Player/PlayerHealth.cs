using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
/**
 * Created by:
 * 
 * Last Edited by: Jared Kessler
 */

=======
>>>>>>> origin/BrandonToTestMainCopy
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
<<<<<<< HEAD
    [SerializeField] private Vector2 spawnPos;
    [SerializeField] private bool whiteHitAnimation;

    [HideInInspector] public RoomTriggerControl currentRoom;

    private AnimationControl playerAnimationControl;
    private PlayerMove playerMovement;
=======
>>>>>>> origin/BrandonToTestMainCopy

    private void Awake()
    {
        currentHealth = maxHealth;
<<<<<<< HEAD
        spawnPos = transform.position;

        playerAnimationControl = GetComponent<AnimationControl>();
        playerMovement = GetComponent<PlayerMove>();
=======
>>>>>>> origin/BrandonToTestMainCopy
    }


    //Handles Damage
    public void Damage(int damage)
    {
        Debug.Log("Taking Damage: " + currentHealth);
        currentHealth -= damage;
<<<<<<< HEAD

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
            
            foreach (GameObject trigger in currentRoom.triggerParentChildren)
            {
                trigger.SetActive(true);
            }

=======
        if (currentHealth <= 0)
        {
>>>>>>> origin/BrandonToTestMainCopy
            Debug.Log("You Lose");
        }
    }
}
