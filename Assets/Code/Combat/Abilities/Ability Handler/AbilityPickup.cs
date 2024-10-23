using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    [Header("Ability Details")]
    [SerializeField] private string abilityToPickupName;  // Name of the ability to pick up
    [SerializeField] private AbilityType abilityType;     // Type of the ability

    private bool playerInRange = false;
    private AbilityManager playerAbilityManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the pickup
        if (collision.gameObject.CompareTag("player"))
        {
            playerInRange = true;        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if player leaves the pickup
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Access the AbilityManager singleton
            AbilityManager abilityManager = AbilityManager.Instance;

            if (abilityManager != null)
            {
                // Assign the ability based on its type (Primary, Secondary, Dash, or Passive)
                abilityManager.Swap(abilityType, abilityToPickupName);

                // Destroy the pickup object after being picked up
                Destroy(gameObject);
            }
        }
    }
}

