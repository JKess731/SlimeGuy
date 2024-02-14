using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerSpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        foreach (GameObject spawner in spawners)
        {
            int indx = Random.Range(0, enemies.Count - 1);

            // Choose an enemy
            GameObject chosenEnemy = enemies[indx];

            // Find a spot in the spawn area
            //Vector2 enemySpawnLoc = chooseSpawnLoc(spawner.GetComponent<CircleCollider2D>());

            //Spawn enemy
            GameObject enemy = Instantiate(chosenEnemy);
            enemy.transform.position = spawner.transform.position;
            enemy.layer = 6;
            //enemy.transform.position = enemySpawnLoc;

        }

        Destroy(gameObject);

    }

}
