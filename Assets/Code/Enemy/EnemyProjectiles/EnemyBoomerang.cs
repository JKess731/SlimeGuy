using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomerang : MonoBehaviour
{
    public float throwForce = 10f;
    public float returnForce = 5f;
    public float maxDistance = 10f;
    public Transform enemy;
    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private GameObject player;
    [SerializeField] private int bulletDamage;
    private bool returning = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        Throw();
    }

    void Throw()
    {
        rb.AddForce(transform.forward * throwForce, (ForceMode2D)ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, initialPosition) >= maxDistance && !returning)
        {
            Return();
        }

    }

    void Return()
    {
        Vector3 direction = (enemy.position - initialPosition).normalized;
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * returnForce, (ForceMode2D)ForceMode.Impulse);
        returning = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(bulletDamage);
        }
    }
}
