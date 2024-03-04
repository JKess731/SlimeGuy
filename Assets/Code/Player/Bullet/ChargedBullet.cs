using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChargedBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage = 5;

    [SerializeField] private float chargeBulletSpeed;
    [SerializeField] private int chargeBulletDamage = 5;
    [SerializeField] private float chargeBulletSize = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(bulletDamage);
            Destroy(gameObject);
        }

        Debug.Log("Hit Enemy");
    }

    public void IncreaseBulletSpeed(float percentage)
    {
        bulletSpeed += (percentage * chargeBulletSpeed);
    }

    public void IncreaseBulletDamage(float percentage)
    {
        bulletDamage += Mathf.FloorToInt(percentage * chargeBulletDamage);
    }

    public void IncreaseBulletSize(float percentage)
    {
        GetComponent<Transform>().localScale = new Vector3(1+ percentage * chargeBulletSize, 1+ percentage * chargeBulletSize, 1);
    }
}
