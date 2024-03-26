using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleEnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    // Update is called once per frame

    private void Awake()
    {
        player = GameObject.FindWithTag("player").gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}
