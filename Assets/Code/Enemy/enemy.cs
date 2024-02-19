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
    [SerializeField] private int defense;

    //Set health to max
    private void Awake()
    {
        health = maxHealth;
    }

    //Handles Damage
    public void Damage(int damage)
    {
        Debug.Log(damage);
        Debug.Log("Taking damage");
        Debug.Log(gameObject.name + ":" + health);
        damage = damage - defense;
        if (damage <= 0) {  damage = 1; }
        health = health - damage;
        Debug.Log("Enemy taking: " + damage);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int getHealth()
    {
        return health;
    }
}
