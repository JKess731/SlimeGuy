using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private GameObject spawnAnimObj;
    [SerializeField] private Vector2 spawnPos = new Vector2(0, 6);
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float spawnDelay = 2f;

    private GameObject bossRef;
    private bool bossSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (bossSpawned == true)
        {
            CheckBoss();
        }
    }

    private IEnumerator Spawn()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(spawnDelay);

        Debug.Log("Spawning");
        GameObject newSpawnObj = Instantiate(spawnAnimObj, spawnPos, Quaternion.identity, transform.parent);
        newSpawnObj.transform.localScale = new Vector2(newSpawnObj.transform.localScale.x * 2, newSpawnObj.transform.localScale.y * 2);

        SpawnAnimation s = newSpawnObj.GetComponent<SpawnAnimation>();
        GameObject boss = Instantiate(bossPrefab, newSpawnObj.transform.position, Quaternion.identity, transform.parent);
        boss.SetActive(false);

        s.enemy = boss;
        bossRef = boss;

        Collider2D c = gameObject.GetComponent<Collider2D>();
        c.enabled = false;
        bossSpawned = true;
    }

    private void CheckBoss()
    {
        if (bossRef == null)
        {
            // Trigger Ending Screen
        }
    }
}
