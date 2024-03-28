using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerControl : MonoBehaviour
{

    [SerializeField] public List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] public bool manual;

    [HideInInspector] public int dangerLevel = 5;
    [HideInInspector] public List<GameObject> spawnedEnemies = new List<GameObject>();
    [HideInInspector] public bool triggerHit = false;
    [HideInInspector] public GameObject triggerParentGameObject;

    private int enemiesDead = 0;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        triggerParentGameObject = GameObject.FindWithTag("trigger_parent");
    }

    private void Start()
    {
        // Level Control
        List<GameObject> l = new List<GameObject>();
        EnemiesInLevel.instance.rooms.Add(gameObject, l);
    }

    private void Update()
    {
        // Update counter for dead enemies
        foreach(GameObject enemy in EnemiesInLevel.instance.rooms[gameObject])
        {
            if (enemy == null)
            {
                enemiesDead++;
            }
        }

        // Clear spawned enemies list when all enemies in the room are dead
        if (enemiesDead == EnemiesInLevel.instance.rooms[gameObject].Count)
        {
            EnemiesInLevel.instance.rooms[gameObject].Clear();
        }

        // If a trigger has been hit, deactivate all remaining triggers in the room
        if (triggerHit)
        {
            triggerParentGameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Spawns enemies at the given spawnerlist
    /// </summary>
    /// <param name="spawnerList"></param>
    public void SpawnEnemies(List<GameObject> spawnerList)
    {
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

                    // Update danger level
                    dangerLeft = dangerLeft - enemyDangeLevel;

                    //Spawn enemy
                    GameObject enemy = Instantiate(chosenEnemy);
                    EnemiesInLevel.instance.rooms[gameObject].Add(enemy);
                    enemy.transform.position = spawner.transform.position;
                    enemy.layer = 7;

                }

            }
        }
        else
        {
            // For loop to spawn manual enemies with spawners
            for (int i = 0; i < spawnerList.Count; i++)
            {
                Debug.Log(i);
                Debug.Log(enemies[i]);
                if (i > enemies.Count)
                {
                    break;
                }
                else
                {
                    GameObject enemy = Instantiate(enemies[i]);
                    enemy.transform.position = spawnerList[i].transform.position;
                    EnemiesInLevel.instance.rooms[gameObject].Add(enemy);
                    enemy.layer = 7;
                }
            }
        }
    }

    /// <summary>
    /// Kills all enemies in the given room
    /// </summary>
    public void KillEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }

        EnemiesInLevel.instance.rooms[gameObject].Clear();
    }

    /// <summary>
    /// Returns the danger level of the given room
    /// </summary>
    /// <returns></returns>
    public int GetDangerLevel()
    {
        return dangerLevel;
    }
}
