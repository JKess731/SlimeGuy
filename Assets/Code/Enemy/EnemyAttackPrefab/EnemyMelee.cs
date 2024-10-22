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

    private void Start()
    {
        _animator.Play("Attack");
    }

    public void Initialize(int damage, float knockback)
    {
        _damage = damage;
        _knockback = knockback;
    }

    //Handles attack collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponent<PlayerStateMachine>().Damage(_damage, transform.right, _knockback, transform.right);
        }
    }

    public void DestroyMelee()
    {
        Destroy(gameObject);
    }
}
