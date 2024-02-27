using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerSpawner : MonoBehaviour
{
    private RoomTriggerControl roomTriggerControl;
    private PlayerHealth health;
    private Player player;

    private void Awake()
    {
        roomTriggerControl = transform.parent.parent.GetComponent<RoomTriggerControl>();

        player = FindAnyObjectByType<Player>();
        health = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        roomTriggerControl.SpawnEnemies(roomTriggerControl.spawners);
        gameObject.SetActive(false);
        health.currentRoom = roomTriggerControl;
    }
}
