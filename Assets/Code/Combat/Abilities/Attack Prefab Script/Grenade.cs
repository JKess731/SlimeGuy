using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class Grenade : Attacks
{
    //Stat variables for grenade

    private Rigidbody2D _rb;
    private float _activationTime;
    private float _speed;
    private float _distance;

    private bool _isStuckToEnemy = false;  // To check if the grenade is stuck to an enemy
    private GameObject _stuckEnemy;        // To reference the enemy it sticks to
    private float _explosionRadius = 2f;   // The radius of the explosion
    private float _explosionDelay = 1f;    // Delay before explosion after sticking to an enemy
    private float _timer;                  // Timer for explosion

    private GameObject _player;
    private GameObject _attack;

    public void Initialize(float damage, float knockback, float activationtime, float speed, float distance)
    {
        _damage = (int)damage;
        _knockback = knockback;
        _activationTime = activationtime;
        _speed = speed;
        _distance = distance;
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("player");
        _attack = GameObject.FindWithTag("attack");
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(_attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector2 forwardDirection = transform.right;  // Assuming the grenade faces right
        _rb.velocity = forwardDirection * _speed;
    }


    private void FixedUpdate()
    {
        // If stuck to an enemy, follow the enemy's position
        if (_isStuckToEnemy && _stuckEnemy != null)
        {
            transform.position = _stuckEnemy.transform.position;
        }
    }

    //Handle collisions with walls and enemies
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //If the grenade hits a wall, destroy the grenade
        if (collision.gameObject.tag == "wall")
        {
            Explode();
        }

        //If the grenade hits an enemy, make it stick to the enemy and then do damage
        if (collision.gameObject.tag == "enemy" && !_isStuckToEnemy)
        {
            StickToEnemy(collision.gameObject);
        }
    }

    // Stick the grenade to the enemy and start the explosion timer
    private void StickToEnemy(GameObject enemy)
    {
        _isStuckToEnemy = true;
        _stuckEnemy = enemy;

        // Stop the grenade's movement
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;  // Make the grenade stick without physics

        _rb.simulated = false;

        transform.SetParent(_stuckEnemy.transform);  // Make the grenade a child of the enemy

        // Start the countdown for the explosion
        StartCoroutine(ExplosionCountdown());
    }

    // Coroutine to handle the delay before explosion
    private IEnumerator ExplosionCountdown()
    {
        yield return new WaitForSeconds(_explosionDelay);  // Wait for 3 seconds
        Destroy(gameObject);
        Explode();  // Trigger explosion after delay
    }

    // Method to trigger explosion and deal damage
    private void Explode()
    {
        // Find all enemies in the explosion radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("enemy"))
            {
                // Apply damage to each enemy within the explosion radius
                enemy.GetComponent<EnemyBase>().Damage(_damage);

                // Apply knockback to each enemy
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    enemyRb.AddForce(knockbackDirection * _knockback, ForceMode2D.Impulse);
                }
            }
        }
        Destroy(gameObject);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
    }
}