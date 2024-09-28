using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Attacks
{
    private float _activationTime;
    private float _rotationSpeed;

    private GameObject _player;
    private StatusSO _status;
    private GameObject _attack;

    private void Start()
    {
        Destroy(gameObject, _activationTime);

        _player = GameObject.FindWithTag("player");
        _attack = GameObject.FindWithTag("attack");

    }

    private void FixedUpdate()
    {
        transform.position = _player.transform.position;
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    public void SetWhipStruct(WhipStruct whipStruct)
    {
        _damage = whipStruct.Damage;
        _knockback = whipStruct.Knockback;
        _activationTime = whipStruct.ActivationTime;
        _rotationSpeed = whipStruct.RotationSpeed;
        _status = whipStruct.Status;
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
