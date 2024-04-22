using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    public Vector2 playerpos = Vector2.zero;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().Damage(bulletDamage);
            Debug.Log("Bullet dmg");
            Destroy(gameObject);
        }
    }
}
