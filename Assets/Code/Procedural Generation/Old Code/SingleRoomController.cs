using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class SingleRoomController : MonoBehaviour
{
    [SerializeField] private GameObject spawnAnimObj;
    [SerializeField] private List<GameObject> roomDoors = new List<GameObject>();
    [SerializeField] private float audioDelay = 3f;
    public List<RoomLevelWave> waves = new List<RoomLevelWave>();

    private int currentWave = 0;
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    #region Classes

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

    #endregion

    #region Events

    public UnityEvent OnRoomTriggered ; // Invoked when a room is triggered
    public UnityEvent OnRoomCleared;   // Invoked when a room is fully cleared
    public UnityEvent OnWaveStart;     // Invoked when a new wave has started
    public UnityEvent OnWaveCleared;   // Invoked when a room with waves > 1 has a wave cleared

    #endregion

    public void Start()
    {
        AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 0);
        AudioManager.instance.IValleyTheme.setParameterByName("enemyNear", 1, false);
        Init();
    }

    private void Init()
    {
        // Initialize Listeners to Events
        OnRoomTriggered.AddListener(LockDoors); // Lock the doors when the room starts
        OnRoomTriggered.AddListener(NextWave); // Start the first wave when triggered

        OnRoomCleared.AddListener(UnlockDoors); // Unlock the doors when the room is cleared
    }

    #region Wave Control

    // Attempts to start the next wave
    private void NextWave()
    {
        currentWave++;
        Debug.Log(currentWave);

        if (currentWave > waves.Count)
        {
            OnRoomCleared?.Invoke(); // No more waves to spawn
        }
        else
        {
            RoomLevelWave waveToSpawn = waves[currentWave - 1];
            StartCoroutine(SpawnEnemies(waveToSpawn.monsters));
        }
    }

    #endregion

    #region Door Control

    private void LockDoors()
    {
        foreach (GameObject d in roomDoors)
        {
            d.SetActive(true);
        }

        AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 1);
    }

    private void UnlockDoors()
    {
        foreach (GameObject d in roomDoors)
        {
            d.SetActive(false);
        }

        AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 2);
        StartCoroutine(AudioDelay(audioDelay));
    }

    #endregion

    #region Enemy Control

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

        OnWaveStart.AddListener(AddDeathListeners); // Listen for enemies when they die
        OnWaveCleared.AddListener(NextWave); // Start the next wave if a wave has cleared
        OnWaveStart?.Invoke();
    }

    // Lets this script listen for when Enemies die
    private void AddDeathListeners()
    {
        foreach (GameObject enemyObj in spawnedEnemies)
        {
            EnemyBase enemy = enemyObj.GetComponent<EnemyBase>();
            enemy.OnDeath.AddListener(delegate { RemoveDeadEnemy(enemy); });
        }
    }

    // Removes the given enemy from the list & checks if the list has no more enemies
    private void RemoveDeadEnemy(EnemyBase e)
    {
        StartCoroutine(DeadDelay(e.gameObject));
    }

    private IEnumerator DeadDelay(GameObject e)
    {
        yield return new WaitForSeconds(.5f);
        spawnedEnemies.Remove(e);
        Debug.Log(spawnedEnemies.Count);
        if (spawnedEnemies.Count <= 0)
        {
            // Tell the controller that this wave has cleared
            OnWaveCleared?.Invoke();
        }
    }

    #endregion

    #region Audio

    private IEnumerator AudioDelay(float t)
    {
        yield return new WaitForSeconds(t);
        AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 0);
    }

    #endregion

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

//Audio Change