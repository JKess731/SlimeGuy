using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Punch : Attacks
{
    private Rigidbody2D _rb;
    private float _range;
    private float _speed;
    private Vector3 _startPosition;
    private float _maxDistance;

    private GameObject _player;
    private GameObject _attack;

    public void Initialize(int damage, float knockback, float speed, float range)
    {
        _damage = damage;
        _knockback = knockback;
        _speed = speed;
        _range = range;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _maxDistance = _range; // Punch range
        _player = GameObject.FindWithTag("player");
        _attack = GameObject.FindWithTag("attack");

        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(_attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Handle collisions with enemies and walls
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        */

        // If the punch hits an enemy, damage the enemy and apply knockback
        if (collision.gameObject.tag == "enemy")
        {
            //Debug.Log("Destroy me");
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage, transform.right, _knockback, Vector2.up);
        }
    }

    private void Update()
    {
        // Move the punch forward
        _rb.velocity = transform.right * _speed;

        // Destroy the punch after it has traveled its max distance
        if (Vector3.Distance(_startPosition, transform.position) >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
