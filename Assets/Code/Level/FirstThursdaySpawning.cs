using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstThursdaySpawning : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private int maxEnemiesAtOnce;

    private List<GameObject> enemies = new List<GameObject>();

    private int currentEnemyCount = 0;

    private HashSet<GameObject> lastTwoSpawners = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentEnemyCount < maxEnemiesAtOnce)
        {
            if (collision.gameObject.tag == "spawn" && !lastTwoSpawners.Contains(collision.gameObject))
            {
                GameObject spawner = collision.gameObject;
                Vector3 pos = collision.transform.position;

                GameObject enemy = Instantiate(enemyToSpawn);
                enemy.transform.position = pos;
                currentEnemyCount++;
                enemies.Add(enemy);
                enemy.layer = 8;

                if (lastTwoSpawners.Count == 2)
                {
                    List<GameObject> theSpawners = new List<GameObject>();
                    foreach (GameObject s in lastTwoSpawners)
                    {
                        theSpawners.Add(s);
                    }
                    theSpawners.RemoveAt(0);
                    lastTwoSpawners.Clear();

                    foreach (GameObject s in theSpawners)
                    {
                        lastTwoSpawners.Add(s);
                    }

                    lastTwoSpawners.Add(collision.gameObject);
                }
                else
                {
                    lastTwoSpawners.Add(collision.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        HandleEnemies();
    }

    private void HandleEnemies()
    {
        List<GameObject> nullEnemies = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                nullEnemies.Add(enemy);
            }
        }

        foreach(GameObject enemy in nullEnemies)
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
                currentEnemyCount--;
            }
        }
    }
}
