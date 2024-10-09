using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Events;

public class SingleRoomController : MonoBehaviour
{
    [System.Serializable]
    public class MonsterSpawner
    {
        public GameObject enemyPrefab;

        [Tooltip("Local Position to the Room Object")]
        public Vector2 spawnPos;
        public float spawnDelay;
    }

    [System.Serializable]
    public class RoomLevelWave
    {
        public List<MonsterSpawner> monsters = new List<MonsterSpawner>();
    }

    [SerializeField] private GameObject spawnAnimObj;
    public RoomTag roomTag;
    [SerializeField] private List<GameObject> roomDoors = new List<GameObject>();
    public List<RoomLevelWave> waves = new List<RoomLevelWave>();

    private int currentWave = 0;
    private bool inWave = false;
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    private bool triggered = false;
    private bool cleared = false;

    private void Update()
    {
        if (triggered)
        {
            CheckWave();
        }
    }

    public void StartNextWave()
    {
        if (currentWave == 0)
        {
            foreach (GameObject door in roomDoors)
            {
                door.SetActive(true);
            }
        }

        RoomLevelWave waveToSpawn = waves[currentWave];
        inWave = true;
        currentWave++;
        StartCoroutine(SpawnEnemies(waveToSpawn.monsters));
    }

    IEnumerator SpawnEnemies(List<MonsterSpawner> waveMonsters)
    {
        foreach (MonsterSpawner ms in waveMonsters)
        {
            yield return new WaitForSecondsRealtime(ms.spawnDelay);

            GameObject spawnAnim = Instantiate(spawnAnimObj, transform);
            spawnAnim.transform.localPosition = ms.spawnPos;

            GameObject enemy = Instantiate(ms.enemyPrefab, transform);
            SpawnAnimation s = spawnAnim.GetComponent<SpawnAnimation>();
            s.enemy = enemy;
            enemy.transform.localPosition = ms.spawnPos;
            enemy.SetActive(false);

            spawnedEnemies.Add(enemy);
        }

        if (triggered == false) triggered = true;

        inWave = true;
    }

    private void CheckNullEnemies()
    {
        if (spawnedEnemies.Count == 0 && inWave)
        {
            inWave = false;

            if (currentWave == waves.Count)
            {
                cleared = true;
            }
        }
        else
        {
            List<GameObject> nullEnemies = new List<GameObject>();

            foreach (GameObject enemy in spawnedEnemies)
            {
                if (enemy == null)
                {
                    nullEnemies.Add(enemy);
                }
            }
            spawnedEnemies.RemoveAll(nullEnemies.Contains);
        }
    }

    private void CheckWave()
    {
        if (inWave)
        {
            CheckNullEnemies();
        }
        else
        {
            if (currentWave < waves.Count)
            {
                StartNextWave();
            }
            else if (currentWave == waves.Count && spawnedEnemies.Count == 0)
            {
                foreach (GameObject door in roomDoors)
                {
                    door.SetActive(false);
                }

                LevelGenerator lg = GameObject.FindAnyObjectByType<LevelGenerator>();
                lg.roomsCleared++;
                lg.clearedChain++;
                lg.lastClearedRoom = this.gameObject;

                //gameObject.SetActive(false);
                triggered = false;
            }
        }
    }

}

public enum RoomTag
{
    EVENT,
    BOSS,
    SHOP,
    CORRIDOR,
    WAVE,
    BASIC,
    EMPTY
}
