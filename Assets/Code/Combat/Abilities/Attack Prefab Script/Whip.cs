using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Attacks
{
    private float _activationTime;
    private float _rotationSpeed;

    private GameObject _player;
    private StatusSO _status;

    public void Initialize(float damage, float knockback, float activationTime, float rotationSpeed, StatusSO status)
    {
        _damage = damage;
        _knockback = knockback;
        _activationTime = activationTime;
        _rotationSpeed = rotationSpeed;
        _status = status;
    }

    public void Initialize(float damage, float knockback, float rotationSpeed, float activationTime)
    {
        _damage = damage;
        _knockback = knockback;
        _rotationSpeed = rotationSpeed;
        _activationTime = activationTime;
    }

    private void Start()
    {
        Destroy(gameObject, _activationTime);

        _player = GameObject.FindWithTag("player");
    }

    private void FixedUpdate()
    {
        //Debug.Log("Spinning");
        transform.position = _player.transform.position;
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage, transform.right, _knockback, Vector2.up);
            if (_status != null)
            {
                //collision.gameObject.GetComponent<StatusManager>().StatusHandler(_status);
            }
        }
    }
}
