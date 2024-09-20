using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : Attacks
{
    private float _activationTime;
    private float _speed;
    private float _distance;

    private GameObject _player;
    private GameObject _attack;

    private void Start()
    {
        Destroy(gameObject, _activationTime);
        _player = GameObject.FindWithTag("player");
        _attack = GameObject.FindWithTag("attack");

        transform.position = _player.transform.position;

        transform.localScale = Vector3.zero;

        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(_attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }

    private void FixedUpdate()
    {
        transform.position = _player.transform.position;

        // Gradually increase the size of the ability until it reaches the maximum size
        if (transform.localScale.x < _distance)
        {
            float growth = _speed * Time.deltaTime;
            transform.localScale += new Vector3(growth, growth, 0);

            // Cap the size to the maximum size
            if (transform.localScale.x > _distance)
            {
                transform.localScale = new Vector3(_distance, _distance, 0);
                Destroy(gameObject);
            }
        }
    }

    public void SetPushStruct(PushStruct pushStruct)
    {
        _damage = pushStruct.Damage;
        _knockback = pushStruct.Knockback;
        _activationTime = pushStruct.ActivationTime;
        _speed = pushStruct.Speed;
        _distance = pushStruct.Distance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Collided with enemy");
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage, transform.right, _knockback, Vector2.up);
        }
    }
}
