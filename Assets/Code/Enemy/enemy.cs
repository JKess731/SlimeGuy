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

    //Handles Damage
    public void Damage(int damage)
    {
        Debug.Log(damage);
        Debug.Log(gameObject.name + ":" + health);
        health = health - damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int getHealth()
    {
        return health;
    }
}
