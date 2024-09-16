using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPulse : Attacks
{
    private float _activationTime;
    private float _speed;
    private float _distance;
    private int _pulseCycles = 3; // Number of times the pulse will grow and shrink

    private GameObject _player;
    private Rigidbody2D _rb;

    private void Start()
    {
        _player = GameObject.FindWithTag("player");
        _rb = GetComponent<Rigidbody2D>();

        transform.position = _player.transform.position;
        transform.localScale = Vector3.zero;

        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        StartCoroutine(PulseRoutine());
    }

    private void FixedUpdate()
    {
        // Update position to follow the player
        transform.position = _player.transform.position + new Vector3(0f, 0.5f, 0f);
    }

    private IEnumerator PulseRoutine()
    {
        float elapsedTime = 0f;
        float cycleTime = _activationTime / _pulseCycles; // Time per cycle

        for (int i = 0; i < _pulseCycles; i++)
        {
            // Grow
            while (elapsedTime < cycleTime)
            {
                float growth = _speed * Time.deltaTime;
                transform.localScale += new Vector3(growth, growth, 0);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.localScale = new Vector3(_distance, _distance, 0);

            // Shrink
            while (elapsedTime < 2 * cycleTime)
            {
                float shrink = _speed * Time.deltaTime;
                transform.localScale -= new Vector3(shrink, shrink, 0);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.localScale = Vector3.zero; // Reset scale for the next cycle
            elapsedTime = 0f; // Reset elapsed time for the next cycle
        }

        Destroy(gameObject); // Destroy after completing cycles
    }

    public void SetPushPulseStruct(PushPulseStruct pushPulseStruct)
    {
        _damage = pushPulseStruct.Damage;
        _knockback = pushPulseStruct.Knockback;
        _activationTime = pushPulseStruct.ActivationTime;
        _speed = pushPulseStruct.Speed;
        _distance = pushPulseStruct.Distance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(_damage, transform.right, _knockback, Vector2.up);
        }
    }
}

