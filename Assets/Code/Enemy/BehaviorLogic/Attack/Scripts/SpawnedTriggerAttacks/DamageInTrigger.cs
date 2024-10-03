using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DamageInTrigger : MonoBehaviour
{

    [SerializeField] private int damageDealt;
    [SerializeField] private float timerEnd;
    [SerializeField] private float timeBetweenDamageInstances;
    [SerializeField] private float knockbackPower = 50;
    private float timeBetweenDamageInstancesTimer;
    private float timer;
    private bool canDamage = true;

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
            collision.gameObject.GetComponent<PlayerStateMachine>().Damage(damageDealt, transform.right, knockbackPower, transform.right);
            canDamage = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            timeBetweenDamageInstancesTimer = 0;
        }

    }
}
