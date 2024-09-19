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
    private StatusSO _status;
    private int _piercingAmount;
    private int _bulletBounce;
    private CircleCollider2D _circleCollider;

    //If the bullet goes out of range, destroy it
    private void Update()
    {
        if (Vector2.Distance(_startPos, transform.position) > _range)
        {
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
                _circleCollider.isTrigger = true;
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
            _circleCollider.isTrigger = true;
            _rb.velocity = transform.right * _speed;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy") { 
            _circleCollider.isTrigger = false;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetBulletStruct(BulletStruct bulletStruct)
    {
        _damage = bulletStruct.Damage;
        _range = bulletStruct.Range;
        _speed = bulletStruct.BulletSpeed;
        _status = bulletStruct.Status;
        _piercingAmount = bulletStruct.piercingAmount;
        _bulletBounce = bulletStruct.bulletBounce;

        _rb.velocity = transform.right * _speed;
    }
}
