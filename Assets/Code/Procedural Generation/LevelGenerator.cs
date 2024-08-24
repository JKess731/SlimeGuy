using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startRoom;
    [SerializeField] private RoomList roomList;
    [SerializeField] private int maxRooms = 10;
    [SerializeField] private int minRooms = 6;
    [SerializeField] private int overrideRoomCount = 0;
    private int chosenRoomCount = 0;
    private int currentRoomCount = 0;

    [Space]
    [SerializeField] private int rowSize = 5;
    [SerializeField] private int colSize = 5;
    [Space]

    [SerializeField] private float spawnDelay = 1f;

    public RoomTypes[,] rooms;

    public List<GameObject> roomsPlaced = new List<GameObject>();

    public Queue<SpawnPoint> spawnPoints = new Queue<SpawnPoint>();

    private class ArrayCoordinate
    {
        public int row = 0;
        public int col = 0;
    }

    private void Start()
    {
        rooms = GenerateGrid();
        PlaceStartRoom();
        chosenRoomCount = GetRoomCount();
        Debug.Log(chosenRoomCount);
    }

    #region Starter Code
    private RoomTypes[,] GenerateGrid()
    {
        return new RoomTypes[rowSize, colSize];
    }

    private int GetRoomCount()
    {
        int count = 0;

        if (overrideRoomCount <= 0)
        {
            count = Random.Range(minRooms, maxRooms + 1);
        }
        else if (overrideRoomCount > 0)
        {
            count = overrideRoomCount;
        }

        return count;
    }

    private void PlaceStartRoom()
    {
        int middleX = rowSize / 2;
        int middleY = colSize / 2;

        GameObject start = Instantiate(startRoom, new Vector2(0, 0), Quaternion.identity);
        RoomTypes rt = start.GetComponent<RoomTypes>();
        rooms[middleX, middleY] = rt;
        roomsPlaced.Add(start);
        currentRoomCount++;

        StartCoroutine(Generate());
    }

    #endregion

    #region Generator Code

    IEnumerator Generate()
    {
        yield return new WaitForSecondsRealtime(spawnDelay / 2);

        while (spawnPoints.Count > 0)
        {

            yield return new WaitForSecondsRealtime(spawnDelay);

            SpawnPoint sp = GetSpawnPoint();
            ArrayCoordinate arrCoords = GetSpawnPointArrayCoords(sp);

            int row = arrCoords.row;
            int col = arrCoords.col;

            DoorTypes doorNeeded = sp.doorNeeded;

            //Debug.Log(row + "," + col);

            if (rooms[row, col] != null )
            {
                // Array Spot is not empty
                // Get reference to the room at the position

                RoomTypes room = rooms[row, col];
                
                // Need to check if room doesnt connect already
                if (!room.doors.Contains(doorNeeded))
                {
                    List<DoorTypes> doors = new List<DoorTypes>();
                    doors.AddRange(room.doors);
                    doors.Add(doorNeeded);

                    List<GameObject> dList = GetDoorAmount(doors.Count);

                    GameObject newRoom = GetNewRoom(doors, dList);

                    RoomTypes oldRoomScript = rooms[row, col];
                    GameObject oldRoomObj = oldRoomScript.gameObject;

                    int index = roomsPlaced.IndexOf(oldRoomObj);
                    roomsPlaced.Remove(oldRoomObj);
                    Destroy(oldRoomObj);

                    GameObject spawnedRoom = Instantiate(newRoom, sp.transform.position, Quaternion.identity);
                    rooms[row, col] = spawnedRoom.GetComponent<RoomTypes>();
                    currentRoomCount++;
                    roomsPlaced.Insert(index, spawnedRoom);
                }

            }
            else
            {
                // Array Spot is empty
                // Need to Pick a room based on the door needed

                GameObject room = GetRoom(doorNeeded, row, col);

                // Instantiate that room at the position of the spawn point
                GameObject spawnedRoom = Instantiate(room, sp.transform.position, Quaternion.identity);

                // Place it in the array at rooms[row, col]
                rooms[row, col] = spawnedRoom.GetComponent<RoomTypes>();
                currentRoomCount++;
                roomsPlaced.Add(spawnedRoom);
            }
        }

        yield return new WaitForSecondsRealtime(1f);

        ChooseBossRoom();

    }

    private GameObject GetRoom(DoorTypes doorNeeded, int row, int col)
    {
        GameObject room = null;
        List<GameObject> options = new List<GameObject>();

        // Step 1: Check if the Room is on an edge

        if (row == 0 || row == rowSize - 1 || col == 0 || col == colSize - 1)
        {
            options = GetDoorAmount(1);

            foreach (GameObject r in options)
            {
                RoomTypes rt = r.GetComponent<RoomTypes>();
                if (rt.doors.Contains(doorNeeded))
                {
                    room = r;
                    break;
                }
            }
        }


        // Step 2: Get Random Room with door type

        else
        {
            if ((chosenRoomCount / 2) + 1 > currentRoomCount)
            {
                options = GetDoorAmount(2);
            }
            else if ((chosenRoomCount / 3) > currentRoomCount)
            {
                options = GetDoorAmount(2);
                options.AddRange(GetDoorAmount(3));
            }
            else
            {
                options = GetDoorAmount(1);
            }

            room = RandomRoom(doorNeeded, options);

        }

        return room;
    }

    private GameObject RandomRoom(DoorTypes dt, List<GameObject> options)
    {
        GameObject room = null;

        List<GameObject> roomTypes = GetRoomsByDoorType(dt, options);

        room = roomTypes.ElementAt(Random.Range(0, roomTypes.Count));

        return room;
    }

    private GameObject GetNewRoom(List<DoorTypes> typesNeeded, List<GameObject> l)
    {
        GameObject room = null;

        foreach (GameObject r in l)
        {
            RoomTypes rt = r.GetComponent<RoomTypes>();
            if (rt.doors.All(typesNeeded.Contains))
            {
                room = r;
                break;
            }    
        }

        return room;
    }

    private SpawnPoint GetSpawnPoint()
    {
        SpawnPoint sp = spawnPoints.Dequeue();

        if (sp.parentRoom == null)
        {
            return GetSpawnPoint();
        }

        return sp;
    }

    private ArrayCoordinate GetSpawnPointArrayCoords(SpawnPoint sp)
    {
        ArrayCoordinate coord = new ArrayCoordinate();

        GameObject parent = sp.parentRoom;

        for (int row = 0; row < rowSize; row++)
        {
            for (int col = 0; col < colSize; col++)
            {
                if (rooms[row, col] == parent.GetComponent<RoomTypes>())
                {
                    coord.row = row;
                    coord.col = col;
                }
            }
        }

        if (sp.doorNeeded == DoorTypes.TOP_DOOR && coord.row < rowSize - 1)
        {
            coord.row += 1;
        }
        else if (sp.doorNeeded == DoorTypes.BOTTOM_DOOR && coord.row > 0)
        {
            coord.row -= 1;
        }
        else if (sp.doorNeeded == DoorTypes.RIGHT_DOOR && coord.col > 0)
        {
            coord.col += 1;
        }
        else if (sp.doorNeeded == DoorTypes.LEFT_DOOR && coord.col < colSize - 1)
        {
            coord.col -= 1;
        }    

        return coord;
    }

    private void ChooseBossRoom()
    {
        GameObject room = roomsPlaced[roomsPlaced.Count - 1];

        for (int i = 0; i < room.transform.childCount; i++)
        {
            if (room.transform.GetChild(i).name == "Walls")
            {
                GameObject r = room.transform.GetChild(i).gameObject;

                for (int j = 0; j < r.transform.childCount; j++)
                {
                    SpriteRenderer sr = r.transform.GetChild(j).GetComponent<SpriteRenderer>();
                    sr.color = Color.red;
                }

                break;
            }
        }

        // Get room grid location
        // Replace room with boss room of same door type from boss room preset list

    }

    #endregion

    #region Room Lists

    private List<GameObject> GetFullSpawnList()
    {
        List<GameObject> AllDoors = new List<GameObject>();
        AllDoors.AddRange(roomList.topRooms);
        AllDoors.AddRange(roomList.bottomRooms);
        AllDoors.AddRange(roomList.leftRooms);
        AllDoors.AddRange(roomList.rightRooms);
        AllDoors.Add(startRoom);

        return AllDoors;
    }

    private List<GameObject> GetDoorAmount(int doorCount)
    {
        List<GameObject> allDoors = GetFullSpawnList();

        List<GameObject> doors = new List<GameObject>();
        foreach (GameObject r in allDoors)
        {
            RoomTypes rt = r.GetComponent<RoomTypes>();
            List<DoorTypes> dt = rt.doors;

            if (dt.Count == doorCount)
            {
                doors.Add(r);
            }
        }

        return doors;
    }

    private List<GameObject> GetRoomsByDoorType(DoorTypes dt, List<GameObject> options)
    {

        List<GameObject> rList = new List<GameObject>();
        foreach (GameObject r in options)
        {
            RoomTypes rt = r.GetComponent<RoomTypes>();
            if (rt.doors.Contains(dt)) rList.Add(r);
        }

        return rList;
    }


    #endregion
}
