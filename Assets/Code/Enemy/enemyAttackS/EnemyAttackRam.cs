using System.Collections;
using UnityEngine;

public class EnemyAttackRam : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] GameObject player;
    private bool attackCon = false;


    //Handles attack collision

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Enter the puddles");
            player.GetComponentInParent<PlayerHealth1>().Damage(damage);
            attackCon = true;
            StartCoroutine(AttackingContinue());
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Exit the puddles");
            attackCon = false;

        }

    }

    IEnumerator AttackingContinue()
    {
        while (attackCon == true)
        {
            Debug.Log("continue attacking");
            player.GetComponentInParent<PlayerHealth1>().Damage(damage);
            yield return new WaitForSeconds(.5f);
        }

    }
}
