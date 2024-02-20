using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerControl : MonoBehaviour
{

    [SerializeField] public List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("player");
    }

    public void SpawnEnemies(List<GameObject> spawnerList)
    {
        SlimeSplit splitAbility = player.GetComponent<SlimeSplit>();

        foreach (GameObject spawner in spawnerList)
        {
            int indx = Random.Range(0, enemies.Count - 1);

            // Choose an enemy
            GameObject chosenEnemy = enemies[indx];

            // Find a spot in the spawn area
            //Vector2 enemySpawnLoc = chooseSpawnLoc(spawner.GetComponent<CircleCollider2D>());

            //Spawn enemy
            GameObject enemy = Instantiate(chosenEnemy);
            enemy.transform.position = spawner.transform.position;
            enemy.layer = 7;

            splitAbility.enemiesInRoom.Add(enemy);

        }
    }
}