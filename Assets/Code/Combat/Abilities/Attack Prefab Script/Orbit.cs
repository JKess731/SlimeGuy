using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : Attacks
{
    private float _activationTime;
    private float _rotationSpeed;
    private float _distance;
    
    private float _angle;
    private float _currentDistance;

    private GameObject _player;
    private GameObject _attack;

    private void Start()
    {
        Destroy(gameObject, _activationTime);
        _player = GameObject.FindWithTag("player");
        _attack = GameObject.FindWithTag("attack");

        _currentDistance = 0f;

        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(_attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void SetInitialAngle(float angle)
    {
        _angle = angle * Mathf.Deg2Rad; // Convert to radians for trigonometric functions
    }

    private void FixedUpdate()
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);
        _angle += _rotationSpeed * Time.deltaTime;

        if (_currentDistance < _distance)
        {
            _currentDistance += Time.deltaTime * (_distance/2);
            if (_currentDistance > _distance)
            {
                _currentDistance = _distance;
            }
        }


        Vector3 orbitPosition = _player.transform.position + offset + new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle)) * _currentDistance;
        transform.position = orbitPosition;
    }

    public void SetOrbitStruct(OrbitStruct orbitStruct)
    {
        _damage = orbitStruct.Damage;
        _knockback = orbitStruct.Knockback;
        _activationTime = orbitStruct.ActivationTime;
        _rotationSpeed = orbitStruct.RotationSpeed;
        _distance = orbitStruct.Distance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            // Ignore collision with the player
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }

        if (collision.gameObject.tag == "wall")
        {
            _rotationSpeed = -_rotationSpeed;
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Collided with enemy");
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage, transform.right, _knockback, Vector2.up);
        }
    }
}
