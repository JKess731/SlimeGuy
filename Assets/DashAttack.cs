using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : Attacks
{
    [Header("Dash Attributes")]
    [SerializeField] private int damage;
    [SerializeField] private float knockback;
    [SerializeField] private float pullRange = 3f; // Range to pull enemy within
    [SerializeField] private float pullForce = 10f; // Force to pull the enemy

    private DashStruct _DashStruct;
    PlayerStats _playerStats;

    // Handle collisions with walls and enemies
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // If the bullet hits a wall, destroy the bullet
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }

        // If the dash hits an enemy, check distance and pull them in if close enough
        if (collision.gameObject.tag == "enemy")
        {
            EnemyBase enemyBase = collision.gameObject.GetComponent<EnemyBase>();

            // Check if enemy is within range
            if (IsEnemyInPullRange(collision.gameObject))
            {
                PullEnemyTowardsPlayer(collision.gameObject);
            }

            // Damage the enemy after pulling them in
            enemyBase.Damage(damage);

            // If the enemy dies, heal the player
            if (enemyBase.isDead)
            {
                HealPlayer();
            }

            Destroy(gameObject);
        }
    }

    private void HealPlayer()
    {
        _playerStats.Heal(_DashStruct.Damage);
    }

    // Method to check if enemy is within pull range
    private bool IsEnemyInPullRange(GameObject enemy)
    {
        float distance = Vector2.Distance(transform.position, enemy.transform.position);
        return distance <= pullRange;
    }

    // Method to pull enemy closer to player
    private void PullEnemyTowardsPlayer(GameObject enemy)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            Vector2 pullDirection = (transform.position - enemy.transform.position).normalized;
            enemyRb.AddForce(pullDirection * pullForce, ForceMode2D.Impulse);
        }
    }
}
