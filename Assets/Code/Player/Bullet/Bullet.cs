using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float KnockbackPower = 1f;
    [SerializeField] private float range = 200f;
    [SerializeField] private int absorption = 1;

    private Vector2 StartPos;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        StartPos = transform.position;

        //Debug.Log("Bullet created");
    }

    private void Update()
    {
        if (Vector2.Distance(StartPos, transform.position) > range)
        {
            Debug.Log(Vector2.Distance(StartPos, transform.position));
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "enemy")
        {
            PlayerStats.instance.IncreaseAbsorption(absorption);
            collision.gameObject.GetComponent<EnemyBase>().Damage(bulletDamage, transform.right, KnockbackPower, transform.right);
            Destroy(gameObject);
        }
    }
}
