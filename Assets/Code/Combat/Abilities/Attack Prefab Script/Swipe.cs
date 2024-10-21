using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Swipe : Attacks
{
    private Rigidbody2D _rb;
    private float _range;
    private float _speed;
    private Vector3 _startPosition;
    private float _maxDistance;

    private float _activationTime;

    private GameObject _player;

    public void Initialize(int damage, float knockback, float activationTime, float speed, float range)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
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
        Destroy(gameObject, _activationTime);
        SlimeSwipeDamage damageScript = GetComponentInChildren<SlimeSwipeDamage>();
        if (damageScript != null)
        {
            damageScript.Initialize(_damage, _knockback);
        }
        _maxDistance = _range; // Swipe range
        _player = GameObject.FindWithTag("player");

        // Ignore collisions with the player itself

        //Removed this line
        //  vvvvvvvv
        //Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void Update()
    {
        // Set the position of the swipe directly without rotating
        Vector3 playerPosition = _player.transform.position;
        Vector3 swipePosition = playerPosition;

        // Move the swipe to the new position
        transform.position = swipePosition;

        // Rotate around the player
        transform.RotateAround(playerPosition, Vector3.forward, _speed * Time.deltaTime);

        // Check if the swipe has traveled its max distance
        if (Vector3.Distance(_startPosition, transform.position) >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    // Remove collision handling from this script
}


