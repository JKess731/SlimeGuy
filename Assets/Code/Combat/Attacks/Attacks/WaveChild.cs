using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChild : Attacks
{
    [Header("Wave Settings")]
    [SerializeField] private float _lifeTime;

    private HashSet<EnemyBase> _enemyHashSet = new  HashSet<EnemyBase>();

    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        // If the object is an enemy
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy hit");

            EnemyBase enemy = collision.GetComponent<EnemyBase>();

            // If the enemy is already in the hashset, return
            if (_enemyHashSet.Contains(enemy))
            {
                return;
            }

            //Else, add the enemy to the hashset and damage it
            enemy.Damage(base.damage);
            _enemyHashSet.Add(enemy);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        // If the object is an enemy
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy hit");

            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

            // If the enemy is already in the hashset, return
            if (_enemyHashSet.Contains(enemy))
            {
                return;
            }

            //Else, add the enemy to the hashset and damage it
            enemy.Damage(base.damage);
            _enemyHashSet.Add(enemy);
        }
    }
}
