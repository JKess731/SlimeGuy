using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunAwayCheck : MonoBehaviour
{
    public GameObject playerTarget { get; set; }
    private EnemyBase enemy;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("player");

        enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            enemy.setRunAwayDistance(true);
        }
    }
}

