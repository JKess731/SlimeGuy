using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jared Kessler
// 2/15/24 1:59 PM
public class MinionMove : MonoBehaviour
{
    private GameObject player;
    public GameObject target = null;

    private SlimeSplit playerSlimeSplit;
    private LookAtEnemy lookAtEnemy;
    private int enemyDistance;

    [HideInInspector] private bool isMovingToEnemy = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        playerSlimeSplit = GameObject.FindWithTag("slime_split").GetComponent<SlimeSplit>();
        lookAtEnemy = playerSlimeSplit.transform.GetChild(0).GetComponent<LookAtEnemy>();

        target = FindNearestEnemy();
    }

    void Update()
    {
        enemyDistance = playerSlimeSplit.enemyDistance;


        // If target is or isn't null and minion isn't moving
        if (target == null && !isMovingToEnemy || target != null && !isMovingToEnemy)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 2 * Time.deltaTime);
            target = FindNearestEnemy();
        }
        

        if (isMovingToEnemy != false)
        {
            // If the enemy is destroyed find a new one
            if (target == null)
            {
                // Remove enemy from the list if it has been destroyed
                if (playerSlimeSplit.enemiesInRoom.Contains(target)) 
                {
                    playerSlimeSplit.enemiesInRoom.Remove(target);
                }

                isMovingToEnemy = false;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 2 * Time.deltaTime);
            }
        }
        else
        {
            target = FindNearestEnemy();
        }
    }

    public GameObject FindNearestEnemy()
    {
        List<GameObject> nullEnemies = new List<GameObject>();
        GameObject target = null;
        List<GameObject> enemies = playerSlimeSplit.enemiesInRoom;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                // Check the distance of the player to the checking enemy, if no enemy selected check within the range
                if (target == null)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <= enemyDistance)
                    {
                        target = enemy;
                        lookAtEnemy.closestEnemy = target.transform;
                    }
                }
                // Otherwise check the distance of the current enemy against the last looked at enemy
                else if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, target.transform.position))
                {
                    target = enemy;
                    lookAtEnemy.closestEnemy = target.transform;
                }
            }
            else
            {
                // Enemies that are in the enemiesInRoom list but are null need to be removed from the list
                nullEnemies.Add(enemy);
            }

        }

        foreach (GameObject gameObj in nullEnemies)
        {
            enemies.Remove(gameObj);
        }

        if (target != null)
        {
            isMovingToEnemy = true;
        }
        return target;
    }
}
