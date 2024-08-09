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
            Destroy(gameObject);
        }

        //If the bullet hits an enemy, damage the enemy and destroy the bullet
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;

        Debug.Log("Bullet Created");
    }

    public void SetBulletStruct(BulletStruct bulletStruct)
    {
        _damage = bulletStruct.Damage;
        _range = bulletStruct.Range;
        _speed = bulletStruct.BulletSpeed;

        _rb.velocity = transform.right * _speed;
        Debug.Log(_damage);
        Debug.Log(_range);
        Debug.Log(_speed);
    }
}
