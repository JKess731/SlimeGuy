using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class Grenade : Attacks
{
    private Rigidbody2D _rb;
    private float _speed;

    private float _explosionRadius = 2f;   // The radius of the explosion
    private float _explosionDelay = 3f;    // Delay before explosion
    private Vector3 _targetPosition;       // The target position (mouse click)
    private Vector3 _startPosition;        // The starting position of the grenade
    private bool _isMoving = false;        // To check if the grenade is moving
    private float _moveTime = 1.5f;        // Time it takes to reach the destination (1.5 seconds)
    private float _elapsedTime = 0f;       // Tracks elapsed time for lerping

    private GameObject _player;
    private GameObject _attack;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;  // Disable physics-based movement since we are controlling it manually

        _player = GameObject.FindWithTag("player");
        _attack = GameObject.FindWithTag("attack");

        // Get the mouse position and convert it to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition = new Vector3(mousePosition.x, mousePosition.y, 0f); // Set z to 0 for 2D

        _startPosition = transform.position; // Grenade starts at the player's position
        _isMoving = true; // Start moving the grenade

        // Ignore collisions with the player and other attacks while moving
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(_attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // Ignore collisions with enemies while moving
        IgnoreEnemyCollisions(true);

        // Start the explosion countdown
        StartCoroutine(ExplosionCountdown());
    }

    private void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;

            // Lerp from the starting position to the target position
            float t = _elapsedTime / _moveTime; // Calculate normalized time (0 to 1)
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, t);

            // Stop moving once we reach the target position
            if (t >= 1f)
            {
                _isMoving = false;

                // Re-enable collisions with enemies and player
                IgnoreEnemyCollisions(false);
                Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            }
        }
    }

    // Coroutine to handle the delay before explosion
    private IEnumerator ExplosionCountdown()
    {
        yield return new WaitForSeconds(_explosionDelay);  // Wait for 3 seconds
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

        // Destroy the grenade after explosion
        Destroy(gameObject);
    }

    // Helper function to ignore or re-enable collisions with enemies
    private void IgnoreEnemyCollisions(bool ignore)
    {
        Collider2D grenadeCollider = GetComponent<Collider2D>();
        Collider2D[] enemyColliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D enemyCollider in enemyColliders)
        {
            if (enemyCollider.CompareTag("enemy"))
            {
                Physics2D.IgnoreCollision(grenadeCollider, enemyCollider, ignore);
            }
        }
    }

    public void SetGrenadeStruct(GrenadeStruct grenadeStruct)
    {
        _damage = grenadeStruct.Damage;
        _speed = grenadeStruct.GrenadeSpeed;
        _knockback = grenadeStruct.Knockback;
    }
}

