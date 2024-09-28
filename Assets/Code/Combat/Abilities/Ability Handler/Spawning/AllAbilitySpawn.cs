using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAbilitySpawner : MonoBehaviour
{
    [Header("Primary Ability Prefabs")]
    [SerializeField] private List<GameObject> primaryAbilityPrefabs;

    [Header("Secondary Ability Prefabs")]
    [SerializeField] private List<GameObject> secondaryAbilityPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private float radius = 3f; // Distance from the player to spawn abilities

    private GameObject player;

    private void Start()
    {
        // Find the player using the "player" tag
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void Update()
    {
        // Check if Shift and P are pressed together
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))
        {
            SpawnAllAbilities();
        }
    }

    private void SpawnAllAbilities()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found! Make sure there is a GameObject with the tag 'player' in the scene.");
            return;
        }

        Vector3 playerPosition = player.transform.position;

        int totalAbilities = primaryAbilityPrefabs.Count + secondaryAbilityPrefabs.Count;
        float angleStep = 360f / totalAbilities; // The angle between each spawned ability

        for (int i = 0; i < primaryAbilityPrefabs.Count; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = CalculateSpawnPosition(playerPosition, angle, radius);
            Instantiate(primaryAbilityPrefabs[i], spawnPosition, Quaternion.identity);
        }

        for (int i = 0; i < secondaryAbilityPrefabs.Count; i++)
        {
            float angle = (primaryAbilityPrefabs.Count + i) * angleStep;
            Vector3 spawnPosition = CalculateSpawnPosition(playerPosition, angle, radius);
            Instantiate(secondaryAbilityPrefabs[i], spawnPosition, Quaternion.identity);
        }
    }

    // Calculate the position around the player in a circular pattern
    private Vector3 CalculateSpawnPosition(Vector3 center, float angle, float radius)
    {
        float radian = angle * Mathf.Deg2Rad;
        float x = center.x + radius * Mathf.Cos(radian);
        float y = center.y + radius * Mathf.Sin(radian);
        return new Vector3(x, y, center.z); // Keep the z-position same as the player
    }
}


