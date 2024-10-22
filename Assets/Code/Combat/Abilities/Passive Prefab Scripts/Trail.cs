using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>(); // Store enemies that have been hit

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("player");

        // Ignore collisions with the player itself
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is tagged as "enemy"
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Check if the enemy has already been hit
            if (!hitEnemies.Contains(collision.gameObject))
            {
                // Deal damage to the enemy
                collision.gameObject.GetComponent<EnemyBase>().Damage(_damage);

                // Add the enemy to the set so it can't be damaged again by this trail prefab
                hitEnemies.Add(collision.gameObject);

                // Ignore future collisions with this enemy
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            }
        }
    }
}

