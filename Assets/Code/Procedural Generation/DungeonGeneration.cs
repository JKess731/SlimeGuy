using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    [SerializeField] private GameObject startingRoom;
    public RoomList roomList;
    public RoomList bossRoomList;

    public List<GameObject> roomsPlaced = new List<GameObject>();

    public void Start()
    {
        Vector2 pos = new Vector2(0, 0);
        Instantiate(startingRoom, pos, Quaternion.identity);
    }

    public void AddRoom(GameObject room)
    {
        roomsPlaced.Add(room);
    }

    public void SetBossRoom()
    {
        int index = roomsPlaced.Count - 1;
        GameObject roomToChange = roomsPlaced[index];


    }
}
