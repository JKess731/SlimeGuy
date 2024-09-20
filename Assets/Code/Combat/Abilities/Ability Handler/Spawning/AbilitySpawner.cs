using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    [Header("Primary Ability Prefabs")]
    [SerializeField] private List<GameObject> primaryAbilityPrefabs;

    [Header("Secondary Ability Prefabs")]
    [SerializeField] private List<GameObject> secondaryAbilityPrefabs;

    [Header("Spawn Points")]
    [SerializeField] private Transform primarySpawnPoint; // The spawn point for the primary ability
    [SerializeField] private Transform[] secondarySpawnPoints; // The spawn points for the secondary abilities

    private void Start()
    {
        SpawnAbilities();
    }

    private void SpawnAbilities()
    {
        // Spawn one random primary ability at the primary spawn point
        if (primaryAbilityPrefabs.Count > 0 && primarySpawnPoint != null)
        {
            int randomPrimaryIndex = Random.Range(0, primaryAbilityPrefabs.Count);
            Instantiate(primaryAbilityPrefabs[randomPrimaryIndex], primarySpawnPoint.position, Quaternion.identity);
        }

        // Spawn two random secondary abilities at the secondary spawn points
        if (secondaryAbilityPrefabs.Count > 0 && secondarySpawnPoints.Length >= 2)
        {
            for (int i = 0; i < 2; i++)
            {
                int randomSecondaryIndex = Random.Range(0, secondaryAbilityPrefabs.Count);
                Instantiate(secondaryAbilityPrefabs[randomSecondaryIndex], secondarySpawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}

