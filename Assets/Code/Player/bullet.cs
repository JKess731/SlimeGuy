using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(bulletDamage);
            Destroy(gameObject);
        }

        Debug.Log("Hit Enemy");
    }
}
