using System.Collections.Generic;
using UnityEngine;

// Jared Kessler
// 2/15/24 1:59 PM
public class MinionMove : MonoBehaviour
{
    private GameObject player;
    public GameObject target = null;

    private SlimeSplit playerSlimeSplit;

    private bool enemyFound = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        playerSlimeSplit = player.GetComponent<SlimeSplit>();
    }

    void Update()
    {
        if (enemyFound == false)
        {
            FindNearestEnemy();
        }
        else
        {
            // If the enemy is destroyed find a new one
            if (target == null)
            {
                // Remove enemy from the list if it has been destroyed
                if (playerSlimeSplit.enemiesInRoom.Contains(target))
                {
                    playerSlimeSplit.enemiesInRoom.Remove(target);
                }

                enemyFound = false;
                transform.position = Vector3.Lerp(transform.position, player.transform.position, 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 2 * Time.deltaTime);
            }
        }
    }

    private void FindNearestEnemy()
    {
        List<GameObject> removeEnemies = new List<GameObject>();

        foreach (GameObject enemy in playerSlimeSplit.enemiesInRoom)
        {
            if (enemy != null)
            {
                // Check the distance of the player to the checking enemy, if no enemy selected check within the range
                if (target == null)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <= 5)
                    {
                        target = enemy;
                    }
                }
                // Otherwise check the distance of the current enemy against the last looked at enemy
                else if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, target.transform.position))
                {
                    target = enemy;
                }

                enemyFound = true;
            }
            else
            {
                removeEnemies.Add(enemy);
            }

        }

        foreach (GameObject gameObj in removeEnemies)
        {
            playerSlimeSplit.enemiesInRoom.Remove(gameObj);
        }
    }
}
