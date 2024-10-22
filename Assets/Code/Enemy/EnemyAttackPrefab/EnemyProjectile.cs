using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyProjectile : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private float _knockback;
    private float _lifetime;

    private Rigidbody2D _rb2d;
    private Animator _animator;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.Play("Start");
    }

    public void Initialize(int damage, float speed, float knockback, float lifeTime)
    {
        _damage = damage;
        _speed = speed;
        _knockback = knockback;
        _lifetime = lifeTime;

        _rb2d.velocity = transform.right * _speed;
        StartCoroutine(StartLifeTime(_lifetime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            DestroyProjectile();
        }

        if (collision.gameObject.CompareTag("player"))
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerStateMachine>().Damage(_damage, knockbackDirection, _knockback, knockbackDirection);
            DestroyProjectile();
        }
    }
    /// <summary>
    /// Destroys the projectile by playing the end animation and setting the velocity to zero
    /// </summary
    private void DestroyProjectile()
    {
        _animator.Play("End");
        _rb2d.velocity = Vector2.zero;
        _rb2d.includeLayers = 0;
    }

    private IEnumerator StartLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyProjectile();
    }

    /// <summary>
    /// Destroys the projectile, used by animation event
    /// </summary>
    public void OnFinished()
    {
        Destroy(gameObject);
    }
}
