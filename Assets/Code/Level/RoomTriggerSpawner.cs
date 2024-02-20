using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerSpawner : MonoBehaviour
{
    private RoomTriggerControl roomTriggerControl;

    private void Awake()
    {
        roomTriggerControl = transform.parent.GetComponent<RoomTriggerControl>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
        
        SlimeSplit splitAbility = player.GetComponent<SlimeSplit>();

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
            enemy.layer = 7;
            
            splitAbility.enemiesInRoom.Add(enemy);

        }
=======
>>>>>>> 58b017070eb7097b7c9b4132a9e047be9107fa2f

        roomTriggerControl.SpawnEnemies(roomTriggerControl.spawners);
        Destroy(gameObject);

    }

}
