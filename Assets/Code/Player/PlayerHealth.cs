using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }


    //Handles Damage
    public void Damage(int damage)
    {
        Debug.Log("Taking Damage: " + currentHealth);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("You Lose");
        }
    }
}
