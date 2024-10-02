using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootCheck : MonoBehaviour
{
    public GameObject playerTarget { get; set; }
    private EnemyBase enemy;

    [SerializeField] private float shootCooldown = 3f;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("player");

        enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            enemy.setShootingDistance(true);
        }
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            enemy.setShootingDistance(false);
        }
    }
    */


    IEnumerator Cooldown()
    {
        Collider2D c = GetComponent<Collider2D>();
        yield return new WaitForSeconds(shootCooldown);
        c.enabled = true;
        Debug.Log("TP COOLDOWN OVER");
    }
}
