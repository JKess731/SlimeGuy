using UnityEngine;

//Edison Li
/* This is a monobehavior class that is attached onto colliders to give them
 * the absorbption ability.
 */

public class Absorption : MonoBehaviour
{
    //Created custom enum for absorbtion buff
    [SerializeField] PlayerScript playerClass;
    [SerializeField] PlayerStatUI playerStatUI;

    [SerializeField] float absorbtionRate = 1f;
    [SerializeField] int growthRate = 1;

    [SerializeField] Stats.statBoost _statBoost;

    private void Start()
    {
        //playerStatUI = FindAnyObjectByType<PlayerStatUI>();
        playerClass = GameObject.FindWithTag("player").GetComponent<PlayerScript>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            int enemyHealth = collision.gameObject.GetComponent<Enemy>().GetHealth();

            if (enemyHealth <= absorbtionRate)
            {
                Absorb(_statBoost);
                //playerStatUI.setText(_statBoost);
                Destroy(collision.gameObject);
            }
        }
    }


    private void Absorb(Stats.statBoost statBoostType)
    {
        playerClass.increaseStats(growthRate, statBoostType);
    }
}