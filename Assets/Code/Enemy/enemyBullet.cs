using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class enemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    public Vector2 playerpos = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "puddles")
        {
            collision.gameObject.GetComponent<playerHealth>().Damage(bulletDamage);
            Debug.Log("Bullet dmg");
            Destroy(gameObject);
        }
    }
}
