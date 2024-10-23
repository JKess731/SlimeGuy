using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWhipDamage : MonoBehaviour
{
    private float _damage;
    private float _knockback;

    private GameObject _player;

    // Method to initialize damage parameters
    public void Initialize(float damage, float knockback)
    {
        _damage = damage;
        _knockback = knockback;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the swipe hits an enemy, handle the damage logic
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Damage the enemy and apply knockback
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage, transform.right, _knockback, Vector2.up);
            //Destroy(gameObject); // Destroy this damage object after applying damage
        }
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("player");

        // Ignore collisions with the player itself
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
