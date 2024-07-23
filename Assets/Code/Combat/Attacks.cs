using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knockback;
    [SerializeField] private float range;

    private Vector2 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(damage, transform.right, knockback, transform.right);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Vector2.Distance(startPos, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
}
