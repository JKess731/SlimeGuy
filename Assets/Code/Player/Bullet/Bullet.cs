using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : Attacks
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float range;

    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
        if (Vector2.Distance(base.startPos, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(base.damage);
            Destroy(gameObject);
        }
    }
}
