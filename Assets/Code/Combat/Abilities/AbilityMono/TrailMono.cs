using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMono : AbilityMonoBase
{
    [SerializeField] private GameObject trailPrefab;
    [SerializeField] private float trailLifetime = 2f;
    [SerializeField] private int maxTrailCount = 8;
    [SerializeField] private float trailSpawnInterval = 0.2f; // Time between trail spawns

    private List<GameObject> trailInstances = new List<GameObject>();
    private Vector2 lastPlayerPosition;
    private Rigidbody2D _playerRb;

    public override void Initialize()
    {
        base.Initialize();
        _playerRb = GameObject.FindWithTag("player").GetComponent<Rigidbody2D>(); // Find the player's Rigidbody using the player tag
        lastPlayerPosition = _playerRb.position; // Store the initial player position
        StartCoroutine(SpawnTrail());
    }

    private IEnumerator SpawnTrail()
    {
        while (true)
        {
            Vector2 currentPlayerPosition = _playerRb.position;

            // Check if the player has moved since the last prefab spawn
            if (currentPlayerPosition != lastPlayerPosition)
            {
                // Spawn a trail prefab at the player's current position
                GameObject newTrail = Instantiate(trailPrefab, _playerRb.position, Quaternion.identity);
                trailInstances.Add(newTrail);

                // Destroy the trail prefab after its lifetime
                Destroy(newTrail, trailLifetime);

                // Remove old trail instances if they exceed the max count
                if (trailInstances.Count > maxTrailCount)
                {
                    Destroy(trailInstances[0]);
                    trailInstances.RemoveAt(0);
                }

                // Update the last known position of the player
                lastPlayerPosition = currentPlayerPosition;
            }

            // Wait for the specified interval before checking again
            yield return new WaitForSeconds(trailSpawnInterval);
        }
    }
}