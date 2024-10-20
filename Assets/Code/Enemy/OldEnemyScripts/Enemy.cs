using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int defense;
    [SerializeField] public int dangerLevel;

    //Set health to max
    private void Awake()
    {
        health = maxHealth;
    }


    //Handles Damage
    public void Damage(int damage)
    {
        //Debug.Log(damage);
        //Debug.Log("Taking damage");
        //Debug.Log(gameObject.name + ":" + health);
        damage = damage - defense;
        if (damage <= 0) {  damage = 1; }
        health = health - damage;

        //Debug.Log("Enemy taking: " + damage);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
