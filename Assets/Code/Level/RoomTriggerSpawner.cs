using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerSpawner : MonoBehaviour
{
    private RoomTriggerControl roomTriggerControl;
    private PlayerStats health;

    private void Awake()
    {
        roomTriggerControl = transform.parent.parent.GetComponent<RoomTriggerControl>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            roomTriggerControl.SpawnEnemies(roomTriggerControl.spawners);
            transform.gameObject.SetActive(false);
            health.currentRoom = roomTriggerControl;

            EnemiesInLevel.instance.currentRoom = roomTriggerControl.gameObject;
        }
    }
}
