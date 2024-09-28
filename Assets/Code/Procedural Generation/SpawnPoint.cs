using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private LevelGenerator genScript;
    public DoorTypes doorNeeded;

    public GameObject parentRoom;

    private void Awake()
    {
        genScript = FindAnyObjectByType<LevelGenerator>();
    }

    private void Start()
    {
        parentRoom = transform.parent.gameObject;
        genScript.spawnPoints.Enqueue(this);

        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSecondsRealtime(10f);
        Destroy(gameObject);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("room_spawn_point") && collision.GetComponent<SpawnPoint>().spawned == true)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Gets the array row and column of the parent room to this spawn point
    /// </summary>
    /// <returns></returns>
    private ArrayCoordinate GetArrayCoordinate()
    {
        ArrayCoordinate arrCoord = new ArrayCoordinate();

        for (int row = 0; row < genScript.rooms.GetLength(0); row++)
        {
            for (int col = 0; col < genScript.rooms.GetLength(1); col++)
            {
                if (genScript.rooms[row, col] == parentRoom)
                {
                    arrCoord.row = row;
                    arrCoord.col = col;
                }
            }
        }

        return arrCoord;
    }

    private void ChooseRoom()
    {
        if (transform.position == Vector3.zero)
        {
            spawned = true;
        }

        if (!spawned)
        {
            ArrayCoordinate parentCoords = GetArrayCoordinate();

            if (doorNeeded == DoorTypes.TOP_DOOR)
            {
                room = roomList.topRooms[Random.Range(0, roomList.topRooms.Length)];

                ArrayCoordinate newRoomCoords = new ArrayCoordinate();
                newRoomCoords.col = parentCoords.col;
                newRoomCoords.row = parentCoords.row + 1;

                if (newRoomCoords.row >= genScript.rooms.GetLength(0) - 1)
                {
                    GameObject singleDoor = GetSingleDoor();
                    GameObject newRoom = Instantiate(singleDoor, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }
                else
                {
                    GameObject newRoom = Instantiate(room, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }
            }

            if (doorNeeded == DoorTypes.BOTTOM_DOOR)
            {
                room = roomList.bottomRooms[Random.Range(0, roomList.bottomRooms.Length)];

                ArrayCoordinate newRoomCoords = new ArrayCoordinate();
                newRoomCoords.col = parentCoords.col;
                newRoomCoords.row = parentCoords.row - 1;

                if (newRoomCoords.row - 1 <= 0)
                {
                    GameObject singleDoor = GetSingleDoor();
                    GameObject newRoom = Instantiate(singleDoor, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }
                else
                {
                    GameObject newRoom = Instantiate(room, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }

            }

            if (doorNeeded == DoorTypes.RIGHT_DOOR)
            {
                room = roomList.rightRooms[Random.Range(0, roomList.rightRooms.Length)];

                ArrayCoordinate newRoomCoords = new ArrayCoordinate();
                newRoomCoords.col = parentCoords.col - 1;
                newRoomCoords.row = parentCoords.row;

                if (newRoomCoords.col <= 0)
                {
                    GameObject singleDoor = GetSingleDoor();
                    GameObject newRoom = Instantiate(singleDoor, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }
                else
                {
                    GameObject newRoom = Instantiate(room, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }
            }

            if (doorNeeded == DoorTypes.LEFT_DOOR)
            {
                room = roomList.leftRooms[Random.Range(0, roomList.leftRooms.Length)];

                ArrayCoordinate newRoomCoords = new ArrayCoordinate();
                newRoomCoords.col = parentCoords.col + 1;
                newRoomCoords.row = parentCoords.row;

                if (newRoomCoords.col >= genScript.rooms.GetLength(1) - 1)
                {
                    GameObject singleDoor = GetSingleDoor();
                    GameObject newRoom = Instantiate(singleDoor, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }
                else
                {
                    GameObject newRoom = Instantiate(room, transform.position, room.transform.rotation);
                    genScript.rooms[newRoomCoords.row, newRoomCoords.col] = newRoom;
                    genScript.roomsPlaced.Add(newRoom);
                }
            }
            spawned = true;
        }
    }

    /// <summary>
    /// Returns a room with only one door based on the doorType needed
    /// </summary>
    /// <returns></returns>
    private GameObject GetSingleDoor()
    {
        if (doorNeeded == DoorTypes.TOP_DOOR)
        {
            foreach (GameObject room in roomList.topRooms)
            {
                RoomTypes roomType = room.GetComponent<RoomTypes>();
                if (roomType.doors.Contains(doorNeeded) && roomType.doors.Count == 1)
                {
                    return room;
                }
            }
        }

        if (doorNeeded == DoorTypes.LEFT_DOOR)
        {
            foreach (GameObject room in roomList.leftRooms)
            {
                RoomTypes roomType = room.GetComponent<RoomTypes>();
                if (roomType.doors.Contains(doorNeeded) && roomType.doors.Count == 1)
                {
                    return room;
                }
            }
        }

        if (doorNeeded == DoorTypes.RIGHT_DOOR)
        {
            foreach (GameObject room in roomList.rightRooms)
            {
                RoomTypes roomType = room.GetComponent<RoomTypes>();
                if (roomType.doors.Contains(doorNeeded) && roomType.doors.Count == 1)
                {
                    return room;
                }
            }
        }

        if (doorNeeded == DoorTypes.BOTTOM_DOOR)
        {
            foreach (GameObject room in roomList.bottomRooms)
            {
                RoomTypes roomType = room.GetComponent<RoomTypes>();
                if (roomType.doors.Contains(doorNeeded) && roomType.doors.Count == 1)
                {
                    return room;
                }
            }
        }

        return null;
    }
    */
}
