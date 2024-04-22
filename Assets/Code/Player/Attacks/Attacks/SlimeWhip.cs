using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWhip : MonoBehaviour
{
    public float activationTime;
    public float rotationSpeed;
    public float Damage;
    public float KnockBackPower;


    private GameObject player;

    private void Start()
    {
        Destroy(gameObject, activationTime);
        player = GameObject.FindWithTag("player");
    }

    private void Update()
    {
        transform.position = player.transform.position;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Hit Enemy");
            collision.gameObject.GetComponent<EnemyBase>().Damage(Damage, transform.right, KnockBackPower, Vector2.up);
        }
    }
}
