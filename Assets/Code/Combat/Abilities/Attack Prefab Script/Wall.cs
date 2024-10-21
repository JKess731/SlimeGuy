using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Wall : Attacks
{
    private Rigidbody2D _rb;
    //private int _wallHealth = 10;  // Wall health, so it can be destroyed
    private Vector3 _startPosition;
    private float _activationTime;

    private Collider2D _wallCollider;
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();  // Store enemies that have been hit
    private bool _isActive = true;

    public void Initialize(float damage, float knockback, float activationtime)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationtime;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _wallCollider = GetComponent<Collider2D>();

        // Destroy the wall after a certain time
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_activationTime);
        Destroy(gameObject);  // Destroy the wall after its lifetime
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive) return;

        // Handle enemy collision
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Apply damage and knockback to enemies only if they haven't been hit by this wall before
            if (!hitEnemies.Contains(collision.gameObject))
            {
                // Deal damage to the enemy
                var enemy = collision.gameObject.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.Damage(_damage);

                    // Apply knockback
                    Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (enemyRb != null)
                    {
                        Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                        enemyRb.AddForce(knockbackDir * _knockback, ForceMode2D.Impulse);
                    }
                }

                // Add the enemy to the hit list to prevent multiple hits
                hitEnemies.Add(collision.gameObject);
            }
        }
    }

    // Method to take damage and reduce wall health
    public void TakeDamage(int damage) // WILL DO THIS EVENTUALLY
    {
        //_wallHealth -= damage;

        //if (_wallHealth <= 0)
        //{
        //    Destroy(gameObject);  // Destroy the wall when health reaches zero
        //}
    }
}


