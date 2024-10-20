using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    [Header("Ability to Assign")]
    [SerializeField] private AbilitySOBase abilityToPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the ability
        if (collision.gameObject.CompareTag("player"))
        {
            // Get the player's AbilityManager
            AbilityManager abilityManager = collision.GetComponent<AbilityManager>();

            if (abilityManager != null)
            {
                // Assign the picked-up ability to the first empty slot (primary or secondary)
                if (abilityToPickup.AbilityType.Equals(AbilityType.PRIMARY))
                {
                    //abilityManager.InstaniatePrimary(abilityToPickup);
                }
                else if (abilityToPickup.AbilityType.Equals(AbilityType.SECONDARY))
                {
                    //abilityManager.InstaniateSecondary(abilityToPickup);
                }

                // Destroy the pickup object after being picked up
                Destroy(gameObject);
            }
        }
    }
}

