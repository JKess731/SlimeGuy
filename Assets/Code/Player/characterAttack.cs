using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

//Edison Li

/**
 * Ability holder class
Controls when the sime abilities are activated
**/
public class characterAttack : MonoBehaviour
{
    //PlayerInfo
    [SerializeField] GameObject player;
    [SerializeField] characterMovement movement;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currenthealth;

    public healthBar healthBar;

    public LayerMask enemyLayer;

    //Absorb
    public Transform attackPointAbsorb;
    public float attackRange;

    //Slingshot
    public Transform attackPointSlingshot;
    public float dashSpeed = 10f;
    public float dashTime = 0.5f;

    //SlimeShotgun
    public Transform attackPointShotgun;
    public GameObject slimeBlob;
    public int bulletCount = 3;
    public float delay = 0.1f;


    /**Trial for different ability holder
    public lightAbility lightAbility;
    public heavyAbility heavyAbility;
    */

    //The ID of the attack
    public int attack1;
    public int attack2;
    private void Start()
    {
        healthBar.SetMaxHealth(10);
        healthBar.SetHealth(currenthealth);
    }
    //Programmed to mouse clicks for now
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack(attack1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Attack(attack2);
        }
    }

    //Attack Switch case
    private void Attack(int attackNumber)
    {
        switch (attackNumber)
        {
            case 1:
                attackNumber = 1;
                Absorb();
                break;
            case 2:
                attackNumber = 2;
                SlimeShotgn();
                break;
            case 3:
                attackNumber = 3;
                Slingshot();
                break;
        }
    }
    private void Absorb()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointAbsorb.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit: " + enemy.name);
            if(currenthealth < maxHealth)
            {
                currenthealth++;
                healthBar.SetHealth(currenthealth);
            }

        }
    }

    private void SlimeShotgn(){
        Quaternion left = attackPointShotgun.rotation * Quaternion.Euler(0f,0f,10f);
        Quaternion right = attackPointShotgun.rotation * Quaternion.Euler(0f, 0f, -10f); ;

        Instantiate(slimeBlob, attackPointShotgun.position, left);
        Instantiate(slimeBlob, attackPointShotgun.position, attackPointShotgun.rotation);
        Instantiate(slimeBlob, attackPointShotgun.position, right);
    }

    //Need to be fixed
    //Kinda buggy
    private void Slingshot()
    {
        StartCoroutine("Dash");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointSlingshot.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit: " + enemy.name);
        }
    }

    private IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            movement.controller.Move(movement.moveDirection * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator ShootSlime(int slimeCount0)
    {
        for (int i = 0; i < slimeCount0; i++)
        {
            Instantiate(slimeBlob, attackPointShotgun);
            yield return delay;
        }
    }
    private void OnDrawGizmos()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPointAbsorb == null)
        {
            return;
        }

        //Draws collision of attack
        Handles.color = Color.red;
        Handles.DrawWireDisc(attackPointAbsorb.position, new Vector3(0, 0, 1), attackRange);
    }
}
