using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Transform target;
    public float knockbackPower;
    public float delay;

    private Rigidbody2D rb;
    private Collider2D teleportTrigger;
    private GameObject enemy;

    private bool canDamage = true;
    private bool isMoving = false;

    public UnityEvent OnCollide;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            OnDeath();
        }

        if (collision.gameObject.CompareTag("player") && canDamage)
        {
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
        this.enemy = enemy;  
        this.damage = damage;
        this.speed = speed;
        this.target = target;
        this.knockbackPower = knockbackPower; 
        this.delay = delay;

        teleportTrigger = enemy.transform.GetChild(0).GetChild(0).GetComponent<Collider2D>();

        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delay);
        isMoving = true;
        teleportTrigger.enabled = true;
        Vector3 direction = target.position - transform.position;

        while (isMoving)
        {
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
            yield return null;
        }
    }
}
