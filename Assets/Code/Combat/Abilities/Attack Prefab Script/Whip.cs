using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Attacks
{
    private Rigidbody2D _rb;
    private float _activationTime;
    private float _rotationSpeed;
    private float _range;
    private Vector3 _startPosition;
    private float _maxDistance;

    private GameObject _player;
    private StatusSO _status;

    public void Initialize(float damage, float knockback, float activationTime, float rotationSpeed, float range, StatusSO status)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
        _rotationSpeed = rotationSpeed;
        _range = range;
        _status = status;
    }

    public void Initialize(float damage, float knockback, float rotationSpeed, float activationTime)
    {
        _damage = damage;
        _knockback = knockback;
        _rotationSpeed = rotationSpeed;
        _activationTime = activationTime;
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
    }

    private void Update()
    {
        // Set the position of the swipe directly without rotating
        Vector3 playerPosition = _player.transform.position;
        Vector3 whipPosition = playerPosition;

        // Move the swipe to the new position
        transform.position = whipPosition;

        // Rotate around the player
        transform.RotateAround(playerPosition, Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
