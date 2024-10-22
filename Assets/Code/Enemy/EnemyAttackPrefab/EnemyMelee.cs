using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMelee : MonoBehaviour
{
    private int _damage;
    private float _knockback;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Initialize(int damage, float knockback)
    {
        _damage = damage;
        _knockback = knockback;
    }

    private void Start()
    {
        _animator.Play("Attack");
    }


    //Handles attack collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerStateMachine>().Damage(_damage, knockbackDirection, _knockback, knockbackDirection);
        }
    }

    public void DestroyMelee()
    {
        Destroy(gameObject);
    }
}
