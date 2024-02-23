using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerSpawner : MonoBehaviour
{
    private RoomTriggerControl roomTriggerControl;

    private void Awake()
    {
        roomTriggerControl = transform.parent.GetComponent<RoomTriggerControl>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        roomTriggerControl.SpawnEnemies(roomTriggerControl.spawners);
        Destroy(gameObject);
    }
}
