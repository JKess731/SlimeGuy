using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleEnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float knockBackPower;

    private KnockBack KnockBack;

    private Rigidbody2D rb;
    private Vector2 faceDir;

    // Update is called once per frame

    private void Awake()
    {
        player = GameObject.FindWithTag("player").gameObject;
        rb = GetComponent<Rigidbody2D>();
        KnockBack = GetComponent<KnockBack>();
    }

    void Update()
    {
        FacePlayer();
        if (!KnockBack.isBeingKnockedBack)
        {
            Move();
        }
    }

    public void Move()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void FacePlayer()
    {
        faceDir = (player.transform.position - transform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            PlayerStateMachine player = collision.gameObject.GetComponent<PlayerStateMachine>();
            player.Damage(1, faceDir, knockBackPower, Vector2.up);
        }
    }
}
