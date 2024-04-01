using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletVolley : MonoBehaviour
{
    private Transform player;
    public int volleyDamage;
    private bool hitAttack = false;
    public float delayVolleyTime;

    private void Awake()
    {
        player = GameObject.FindWithTag("player").transform;
        Debug.Log(delayVolleyTime);
        StartCoroutine(delayVolley(delayVolleyTime));
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            hitAttack = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            hitAttack = false;
        }

    }

    IEnumerator delayVolley(float delayVolleyTime) {
        Debug.Log("Bullet starts");
        yield return new WaitForSeconds(delayVolleyTime/2);
        if (hitAttack == true) {
            player.GetComponent<PlayerHealth>().Damage(volleyDamage);
        }
        yield return new WaitForSeconds(delayVolleyTime / 2);
        Debug.Log("Bullet dies");
        Destroy(gameObject);

    }
}
