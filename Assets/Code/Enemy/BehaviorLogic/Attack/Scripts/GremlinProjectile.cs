using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GremlinProjectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Transform target;
    public float knockbackPower;
    public float delay;

    private Rigidbody2D rb;
    private bool canDamage = true;
    private bool isMoving = false;

    public UnityEvent OnCollide;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") && canDamage)
        {
            Debug.Log("Player Hit");
            canDamage = false;
            PlayerStateMachine psm = collision.gameObject.GetComponent<PlayerStateMachine>();

            psm.Damage(damage, transform.right, knockbackPower, transform.right);

            OnDeath();
        }
    }

    public void OnDeath()
    {
        OnCollide?.Invoke();
        StopCoroutine(Shoot());
        Destroy(gameObject); 
    }

    public void StartShoot(GameObject enemy, int damage, float speed, Transform target,
        float knockbackPower, float delay)
    {
        this.damage = damage;
        this.speed = speed;
        this.target = target;
        this.knockbackPower = knockbackPower;
        this.delay = delay;

        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delay);
        isMoving = true;
        Vector3 direction = (target.position - transform.position).normalized;
        while (isMoving)
        {
            rb.velocity = direction * speed;
            yield return null;
        }
    }
}