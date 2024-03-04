using UnityEngine;

public class RoomTriggerSpawner : MonoBehaviour
{
    private RoomTriggerControl roomTriggerControl;
    private PlayerHealth health;
    private PlayerScript player;

    private void Awake()
    {
        roomTriggerControl = transform.parent.parent.GetComponent<RoomTriggerControl>();

        player = FindAnyObjectByType<PlayerScript>();
        health = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        roomTriggerControl.SpawnEnemies(roomTriggerControl.spawners);
        gameObject.SetActive(false);
        health.currentRoom = roomTriggerControl;
    }
}
