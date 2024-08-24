using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject startingRoom;
    public RoomList roomPresetList;

    [SerializeField] private int gridRows = 5;
    [SerializeField] private int gridColumns = 5;
    [SerializeField] private float changeRoomBufferTime = 5f;

    [HideInInspector] public bool changingRooms = false;

    public GameObject[,] rooms;
    public List<GameObject> roomsPlaced = new List<GameObject>();

    private void Start()
    {
        rooms = GenerateGrid();
        PlaceStartRoom();
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

        StartCoroutine(CheckRooms());
    }

    IEnumerator CheckRooms()
    {
        yield return new WaitForSecondsRealtime(changeRoomBufferTime);
        changingRooms = true;

        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                if (rooms[row, col] != null && rooms[row, col] != startingRoom)
                {
                    GameObject room = rooms[row, col];
                    RoomTypes roomTypesScript = room.GetComponent<RoomTypes>();

                    List<DoorTypes> roomDoors = roomTypesScript.doors;
                    List<DoorTypes> connectedDoors = new List<DoorTypes>();

                    foreach (DoorTypes type in roomDoors)
                    {
                        if (type == DoorTypes.TOP_DOOR) // Need Bottom door + 1 to row
                        {
                            int roomCheckRow = row - 1;

                            if (roomCheckRow >= 0 && rooms[roomCheckRow, col] != null)
                            {
                                GameObject topRoom = rooms[roomCheckRow, col];
                                RoomTypes topRoomTypesScript = topRoom.GetComponent<RoomTypes>();

                                List<DoorTypes> topRoomDoors = topRoomTypesScript.doors;

                                if (topRoomDoors.Contains(DoorTypes.BOTTOM_DOOR))
                                {
                                    connectedDoors.Add(DoorTypes.TOP_DOOR);
                                }
                            }
                        }

                        if (type == DoorTypes.BOTTOM_DOOR) // Need Top door - 1 to row
                        {
                            int roomCheckRow = row + 1;

                            if (roomCheckRow < gridRows && rooms[roomCheckRow, col] != null)
                            {
                                GameObject bottomRoom = rooms[roomCheckRow, col];
                                RoomTypes bottomRoomTypesScript = bottomRoom.GetComponent<RoomTypes>();

                                List<DoorTypes> bottomRoomDoors = bottomRoomTypesScript.doors;

                                if (bottomRoomDoors.Contains(DoorTypes.TOP_DOOR))
                                {
                                    connectedDoors.Add(DoorTypes.BOTTOM_DOOR);
                                }
                            }
                        }

                        if (type == DoorTypes.RIGHT_DOOR) // Need Left Door + 1 to col
                        {
                            int roomCheckCol = col + 1;

                            if (roomCheckCol < gridColumns && rooms[row, roomCheckCol] != null)
                            {
                                GameObject rightRoom = rooms[row, roomCheckCol];
                                RoomTypes rightRoomTypes = rightRoom.GetComponent<RoomTypes>();

                                List<DoorTypes> rightRoomDoors = rightRoomTypes.doors;

                                if (rightRoomDoors.Contains(DoorTypes.LEFT_DOOR))
                                {
                                    connectedDoors.Add(DoorTypes.RIGHT_DOOR);
                                }
                            }
                        }   
                        
                        if (type == DoorTypes.LEFT_DOOR) // Need Right Door - 1 to col
                        {
                            int roomCheckCol = col - 1;

                            if (roomCheckCol >= 0 && rooms[row, roomCheckCol] != null)
                            {
                                GameObject leftRoom = rooms[row, roomCheckCol];
                                RoomTypes leftRoomTypes = leftRoom.GetComponent<RoomTypes>();

                                List<DoorTypes> leftRoomDoors = leftRoomTypes.doors;

                                if (leftRoomDoors.Contains(DoorTypes.RIGHT_DOOR))
                                {
                                    connectedDoors.Add(DoorTypes.LEFT_DOOR);
                                }
                            }
                        }
                    }

                    if (connectedDoors.Count == 1)
                    {
                        for (int i = 0; i < roomDoors.Count; i++)
                        {
                            if (connectedDoors.Contains(roomDoors[i]))
                            {
                                Vector2 pos = rooms[row, col].transform.position;

                                GameObject changeRoom = new GameObject();

                                if (connectedDoors.Count == 1)
                                {
                                    changeRoom = GetSingleDoor(roomDoors[i]);
                                }
                                else if (connectedDoors.Count == 2)
                                {
                                    changeRoom = GetDoubleDoor(connectedDoors);
                                }

                                GameObject newRoom = Instantiate(changeRoom, pos, Quaternion.identity);

                                int roomIndex = roomsPlaced.IndexOf(rooms[row, col]);
                                GameObject roomDestroy = rooms[row, col];
                                roomsPlaced.Remove(roomDestroy);
                                Destroy(roomDestroy);
                                rooms[row, col] = newRoom;
                                roomsPlaced.Insert(roomIndex, newRoom);
                            }
                        }
                    }

                }
            }
        }
    }    

    private GameObject GetSingleDoor(DoorTypes doorNeeded)
    {
        if (doorNeeded == DoorTypes.TOP_DOOR)
        {
            foreach (GameObject room in roomPresetList.topRooms)
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
            foreach (GameObject room in roomPresetList.leftRooms)
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
            foreach (GameObject room in roomPresetList.rightRooms)
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
            foreach (GameObject room in roomPresetList.bottomRooms)
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

    private GameObject GetDoubleDoor(List<DoorTypes> doorsNeeded)
    {
        GameObject result = new GameObject();

        List<GameObject> AllDoors = new List<GameObject>();
        AllDoors.AddRange(roomPresetList.topRooms);
        AllDoors.AddRange(roomPresetList.bottomRooms);
        AllDoors.AddRange(roomPresetList.leftRooms);
        AllDoors.AddRange(roomPresetList.rightRooms);

        foreach (GameObject room in AllDoors)
        {
            RoomTypes types = room.GetComponent<RoomTypes>();
            if (types.doors.Count == 2)
            {
                int count = 0;
                foreach (DoorTypes door in types.doors)
                {
                    if (doorsNeeded.Contains(door))
                    {
                        count++;
                    }
                }

                if (count == 2)
                {
                    result = room;
                    break;
                }
            }
        }

        return result;
    }
}
