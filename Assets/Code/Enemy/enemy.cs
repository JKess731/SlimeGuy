using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
public class enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    [SerializeField] private int attackDamage;
    [SerializeField] private float speed;

    //Set health to max
    private void Awake()
    {
        health = maxHealth;
    }

    //Handles attack collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            Damage(1);
        }
    }

    //Handles Damage
    private void Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
