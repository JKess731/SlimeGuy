using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerControl : MonoBehaviour
{
    private int enemiesDead = 0;

    [SerializeField] public List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private GameObject player;
    [HideInInspector] public int dangerLevel = 5;
    [HideInInspector] public List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] public bool manual;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }

    private void Update()
    {
        foreach(GameObject enemy in spawnedEnemies)
        {
            if (enemy == null)
            {
                enemiesDead++;
            }
        }

        if (enemiesDead == spawnedEnemies.Count)
        {
            spawnedEnemies.Clear();
        }
    }

    public void SpawnEnemies(List<GameObject> spawnerList)
    {
        SlimeSplit splitAbility = player.GetComponent<SlimeSplit>();


        // Random enemy spawners
        if (manual != true)
        {
            List<GameObject> canSpawnEnemies = new List<GameObject>();
            canSpawnEnemies.AddRange(enemies);

            int dangerLeft = dangerLevel;

            foreach (GameObject spawner in spawnerList)
            {

                int indx = Random.Range(0, canSpawnEnemies.Count);

                // Choose an enemy
                GameObject chosenEnemy = canSpawnEnemies[indx];
                int enemyDangeLevel = chosenEnemy.GetComponent<Enemy>().dangerLevel;

                /* If enemy danger level is greater than danger left, remove it from
                 * the available enemies to spawn list and choose a new one
                 */
                if (enemyDangeLevel > dangerLeft)
                {
                    canSpawnEnemies.Remove(chosenEnemy);
                    //break;
                }
                else if (dangerLeft <= 0)
                {
                    break;
                }
                else
                {

                    dangerLeft = dangerLeft - enemyDangeLevel;


                    // Find a spot in the spawn area
                    //Vector2 enemySpawnLoc = chooseSpawnLoc(spawner.GetComponent<CircleCollider2D>());

                    //Spawn enemy
                    GameObject enemy = Instantiate(chosenEnemy);
                    spawnedEnemies.Add(enemy);
                    enemy.transform.position = spawner.transform.position;
                    enemy.layer = 7;

                    splitAbility.enemiesInRoom.Add(enemy);

                }

            }
        }
        else
        {
            // For loop to spawn manual enemies with spawners
            for (int i = 0; i < spawnerList.Count - 1; i++)
            {
                if (i > enemies.Count)
                {
                    break;
                }
                else
                {
                    GameObject enemy = Instantiate(enemies[i]);
                    enemy.transform.position = spawnerList[i].transform.position;
                    spawnedEnemies.Add(enemy);
                    enemy.layer = 7;
                    splitAbility.enemiesInRoom.Add(enemy);
                }
            }
        }
    }

    public void KillEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }

        spawnedEnemies.Clear();
    }

    public int GetDangerLevel()
    {
        return dangerLevel;
    }
}
