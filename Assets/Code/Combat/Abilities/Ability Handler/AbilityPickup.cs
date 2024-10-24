using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    [Header("Ability Details")]
    [SerializeField] private string abilityToPickupName;  
    [SerializeField] private AbilityType abilityType;     

    private bool playerInRange = false;
    private AbilityManager playerAbilityManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            AbilityManager abilityManager = AbilityManager.Instance;

            Debug.Log(abilityType);
            
            abilityManager.Swap(abilityType, abilityToPickupName);

            Destroy(gameObject);
        }
    }
}

