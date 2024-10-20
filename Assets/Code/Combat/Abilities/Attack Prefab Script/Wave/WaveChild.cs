using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChild : Attacks
{
    private float _activationTime;
    private StatusSO _status;
    private HashSet<EnemyBase> _enemyHashSet = new  HashSet<EnemyBase>();

    protected void Start()
    {
        Destroy(gameObject, _activationTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
            enemy.Damage(base._damage);
            if (_status != null)
            {
                collision.gameObject.GetComponent<StatusManager>().StatusHandler(_status);
            }
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
            enemy.Damage(base._damage);
            if (_status != null)
            {
                collision.gameObject.GetComponent<StatusManager>().StatusHandler(_status);
            }
            _enemyHashSet.Add(enemy);
        }
    }

    public void SetWaveStruct(WaveStruct waveStruct)
    {
        _damage = waveStruct.Damage;
        _knockback = waveStruct.Knockback;
        _activationTime = waveStruct.ActivationTime;
        _status = waveStruct.Status;
    }
}
