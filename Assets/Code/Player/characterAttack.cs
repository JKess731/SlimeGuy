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

    public LayerMask enemyLayer;

    //Absorb
    public Transform attackPoint;
    public float attackRange;

    //Slingshot
    public float dashSpeed = 10f;
    public float dashTime = 0.5f;

    //SlimeShotgun

    /**Trial for different ability holder
    public lightAbility lightAbility;
    public heavyAbility heavyAbility;
    */

    //The ID of the attack
    public int attack1;
    public int attack2;

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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit: " + enemy.name);
            if(currenthealth < maxHealth)
            {
                currenthealth++;
            }
        }
    }

    private void SlimeShotgn()
    {

    }

    private void Slingshot()
    {
        StartCoroutine("Dash");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

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
            movement.controller.Move(movement.direction * dashSpeed * Time.deltaTime);

            //BAD BAD IDEA
            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        //Draws collision of attack
        Handles.color = Color.red;
        Handles.DrawWireDisc(attackPoint.position, new Vector3(0, 0, 1), attackRange);
    }
}
