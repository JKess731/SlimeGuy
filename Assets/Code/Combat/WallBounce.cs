using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    private Collider2D _collider;
    private int _bulletBounce;
    private Rigidbody2D _rb;
    private float _speed;

    private void Awake()
    {
        _rb = gameObject.GetComponentInParent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    public void Initialize(int bulletBounce, float speed)
    {
        _collider.enabled = true;
        _bulletBounce = bulletBounce;
        _speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            if (_bulletBounce == 0)
            {
                Destroy(gameObject);
            }

            var firstContact = collision.contacts[0];
            Vector2 newVelocity = Vector2.Reflect(transform.right, firstContact.normal);
            _rb.velocity = newVelocity * _speed;
            _rb.rotation = Mathf.Atan2(newVelocity.y, newVelocity.x) * Mathf.Rad2Deg;
            _bulletBounce--;
        }
    }
}
