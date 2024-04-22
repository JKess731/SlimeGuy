using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : Attack
{
    [Header("Trail Attributed")]
    // Using a dictionary for the trail pools, can be useful for switching between types of trails to use
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    [SerializeField] private Transform poolParent;
    [SerializeField] private List<TrailType> trailTypes;
    private List<GameObject> trailsSpawned;
    [SerializeField] private float distanceBetweenObjects = .35f;

    public int trailLength = 10;

    [Serializable]
    public class TrailType
    {
        public string identifier;
        public GameObject trailPrefab;
        public int poolSize;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        trailsSpawned = new List<GameObject>();

        foreach (TrailType trail in trailTypes)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < trail.poolSize; i++)
            {
                GameObject trailPrefab = Instantiate(trail.trailPrefab);
                trailPrefab.SetActive(false);

                trailPrefab.transform.parent = poolParent;

                objPool.Enqueue(trailPrefab);
            }

            poolDictionary.Add(trail.identifier, objPool);
        }
    }

    private void FixedUpdate()
    {
        CheckTrail(trailsSpawned);
    }


    private GameObject SpawnTrailFromQueue(string identifier, Vector2 position)
    {
        if (!poolDictionary.ContainsKey(identifier))
        {
            Debug.LogWarning("identifier not available: " + identifier);
            return null;
        }

        GameObject trailObjToSpawn = poolDictionary[identifier].Dequeue();

        trailObjToSpawn.transform.position = position;
        trailObjToSpawn.SetActive(true);

        poolDictionary[identifier].Enqueue(trailObjToSpawn);

        return trailObjToSpawn;
    }

    private IEnumerator SpawnTrail(float activationTime)
    {
        
        GameObject trailObj = SpawnTrailFromQueue(trailTypes[0].identifier, player.transform.position);
        trailsSpawned.Add(trailObj);

        yield return new WaitForSeconds(activationTime);
    }

    private void CheckTrail(List<GameObject> trailList)
    {
        if (trailList.Count > 0)
        {
            if (Vector2.Distance(trailList[trailList.Count - 1].transform.position, player.transform.position) >= distanceBetweenObjects)
            {
                if (trailsSpawned.Count < trailLength + 1)
                {
                    StartCoroutine(SpawnTrail(activationTime));
                }
                else
                {
                    trailList[0].SetActive(false);
                }
            }

            if (trailList[0].activeSelf == false)
            {
                trailList.RemoveAt(0);
            }
        }
        else
        {
            StartCoroutine(SpawnTrail(activationTime));
        }
    }
}
