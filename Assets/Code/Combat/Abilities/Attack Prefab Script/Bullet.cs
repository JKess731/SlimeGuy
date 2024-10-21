using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : Attacks
{
    //Stat variables of a bullet
    private Rigidbody2D _rb;

    private float _range;
    private float _speed;
    private int _piercingAmount;
    private int _bulletBounce;
    private StatusSO _status;

    private CircleCollider2D _circleCollider;
    private WallBounce _wallBounce;

    //Initialize the bullet with the stats of the bullet
    public void Initialize(float damage, float knockback, float speed, float range, int piercingAmount, int bulletBounce)
    {
        _damage = damage;
        _knockback = knockback;
        _range = range;
        _speed = speed;
        _piercingAmount = piercingAmount;

        if (_piercingAmount > 0)
        {
            _rb.isKinematic = true;
            _circleCollider.isTrigger = true;
        }

        _bulletBounce = bulletBounce;

        if (_bulletBounce > 0)
        {
            _wallBounce.Initialize(_bulletBounce, _speed);
        }

        _rb.velocity = transform.right * _speed;

        Debug.Log("Bullet Initialized");
    }

    //If the bullet goes out of range, destroy it
    private void Update()
    {
        if (Vector2.Distance(_startPos, transform.position) > _range)
        {
            Debug.Log("Bullet out of range");
            Destroy(gameObject);
        }
    }

    //Handle collisions with walls and enemies
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //If the bullet hits a wall, destroy the bullet
        if (collision.gameObject.tag == "wall")
        {
            if (_bulletBounce == 0)
            {
                Destroy(gameObject);
            }
            
             var firstContact = collision.contacts[0];
             Vector2 newVelocity = Vector2.Reflect(transform.right, firstContact.normal);
             _rb.velocity = newVelocity * _speed;
             _bulletBounce--;
        }

        //If the bullet hits an enemy, damage the enemy and destroy the bullet
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage);
            if(_piercingAmount > 0) { 
                _piercingAmount--;
            }
            if (_status != null)
            {
                collision.gameObject.GetComponent<StatusManager>().StatusHandler(_status);
            }
            //Debug.Log("I got past StatusManager");
            if (_piercingAmount == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            if (_piercingAmount > 0)
            {
                collision.gameObject.GetComponent<EnemyBase>().Damage(_damage);
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
        _circleCollider = GetComponent<CircleCollider2D>();
        _wallBounce = gameObject.GetComponentInChildren<WallBounce>();
    }
}
