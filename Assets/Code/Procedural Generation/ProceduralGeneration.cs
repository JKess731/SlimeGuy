using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject startingRoom;
    public RoomList roomPresetList;

    [SerializeField] private int gridRows = 5;
    [SerializeField] private int gridColumns = 5;
    [SerializeField] private float changeRoomBufferTime = 5f;

    public GameObject[,] rooms;
    public List<GameObject> roomsPlaced = new List<GameObject>();

    private void Start()
    {
        rooms = GenerateGrid();
        PlaceStartRoom();
        //StartCoroutine(CheckRooms());
    }

    private GameObject[,] GenerateGrid()
    {
        return new GameObject[gridRows, gridColumns];
    }

    private void PlaceStartRoom()
    {
        int middleX = gridRows / 2;
        int middleY = gridColumns / 2;

        GameObject start = Instantiate(startingRoom, new Vector2(0, 0), Quaternion.identity);
        rooms[middleX, middleY] = start;
        roomsPlaced.Add(start);
    }

}
