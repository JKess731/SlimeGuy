using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DividingSlash : Attacks
{
    private Rigidbody2D _rb;
    private float _range;
    private float _speed;
    private Vector3 _startPosition;
    private float _maxDistance;

    private GameObject _player;
    private GameObject _attack;
    private Rigidbody2D _playerRb;  // Rigidbody of the player
    private float _playerPushDuration = 0.1f; // Duration of the push
    private float _playerPushSpeed = 10f;  // Speed of the player's forward push

    private Vector2 _pushDirection; // Direction to push the player

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _maxDistance = _range; // Slash range
        _player = GameObject.FindWithTag("player");
        _attack = GameObject.FindWithTag("attack");
        _playerRb = _player.GetComponent<Rigidbody2D>();

        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(_attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // Determine the push direction based on the slash direction
        _pushDirection = transform.right; // Use the direction of the slash

        // Push the player forward
        StartCoroutine(PushPlayerForward());
    }

    private IEnumerator PushPlayerForward()
    {
        Vector2 pushVector = _pushDirection * _playerPushSpeed;
        float elapsedTime = 0f;

        // Push the player forward for a short duration
        while (elapsedTime < _playerPushDuration)
        {
            _playerRb.velocity = pushVector;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Stop player's forward movement after the push
        _playerRb.velocity = Vector2.zero;
    }

    // Handle collisions with enemies and walls
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }

        // If the slash hits an enemy, damage the enemy and apply knockback
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("Destroy me");
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage, transform.right, _knockback, Vector2.up);
        }
    }

    private void Update()
    {
        // Move the slash forward
        _rb.velocity = transform.right * _speed;

        // Destroy the slash after it has traveled its max distance
        if (Vector3.Distance(_startPosition, transform.position) >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetDividingSlashStruct(DividingSlashStruct dividingSlashStruct)
    {
        _damage = dividingSlashStruct.Damage;
        _range = dividingSlashStruct.Range;
        _speed = dividingSlashStruct.Speed;
        _knockback = dividingSlashStruct.Knockback;
    }
}

