using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DamageInTrigger : MonoBehaviour
{

    [SerializeField] private int damageDealt;
    private GameObject player;
    [SerializeField] private float timerEnd;
    [SerializeField] private float timeBetweenDamageInstances;
    private float timeBetweenDamageInstancesTimer;
    private float timer;
    private bool canDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
        Debug.Log("SpawnedTrigger");

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > timerEnd)
        {
            Destroy(this.gameObject);
        }
        if(timeBetweenDamageInstancesTimer > timeBetweenDamageInstances)
        {
            timeBetweenDamageInstancesTimer = 0;
            canDamage = true;

        }
        timer += Time.deltaTime;
        timeBetweenDamageInstancesTimer += Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player" && canDamage == true)
        {
            player.GetComponentInParent<PlayerHealth>().Damage(damageDealt);
            canDamage = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            timeBetweenDamageInstancesTimer = 0;
            canDamage = true;
        }

    }
}
