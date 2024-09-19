using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    // Serialized Settings
    [Header("Room Settings")]
    [SerializeField] private GameObject startRoom;
    [SerializeField] private RoomList roomList;
    [SerializeField] private RoomList emptyRoomsList;
    [SerializeField] private int maxRooms = 10; // <= GET EXACT NUMBER NEXT
    [SerializeField] private int minRooms = 6;

    // private settings
    private int overrideRoomCount = 0; // <= REMOVE SOON
    private int chosenRoomCount = 0;
    private int currentRoomCount = 0;

    // data
    [Space]
    [Header("Array Bounds")]
    [SerializeField] private int rowSize = 5;
    [SerializeField] private int colSize = 5;
    [Space]

    // Delay between spawning each room
    [Header("Spawn Settings")]
    [SerializeField] private float spawnDelay = 1f;

    // Loading Screen
    private Canvas mainUi;
    private Canvas loadingUi;
    private GameObject player;

    // Collections
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

        LoadUI();
    }

    #region UI

    private void LoadUI()
    {
        player = GameObject.FindGameObjectWithTag("player");
        mainUi = GameObject.FindGameObjectWithTag("main_ui").GetComponent<Canvas>();
        loadingUi = GameObject.FindGameObjectWithTag("loadingUi").GetComponent<Canvas>();

        mainUi.enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        loadingUi.enabled = true;
    }

    private void GenComplete()
    {
        player.GetComponent<PlayerController>().enabled = true;
        mainUi.enabled = true;
        loadingUi.enabled = false;
    }

    #endregion

    #region Starter Code
    private RoomTypes[,] GenerateGrid()
    {
        return new RoomTypes[rowSize, colSize];
    }

    private int GetRoomCount()
    {
        return Random.Range(minRooms, maxRooms + 1);
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

            if (rooms[row, col] != null)
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

                    GameObject spawnedRoom = Instantiate(newRoom, sp.transform.position, Quaternion.identity, transform);
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
                Debug.Log(row + " " + col);

                // Instantiate that room at the position of the spawn point
                GameObject spawnedRoom = Instantiate(room, sp.transform.position, Quaternion.identity, transform);

                // Place it in the array at rooms[row, col]
                rooms[row, col] = spawnedRoom.GetComponent<RoomTypes>();
                currentRoomCount++;
                roomsPlaced.Add(spawnedRoom);
            }
        }

        yield return new WaitForSecondsRealtime(1f);

        Debug.Log("Generation Complete...");
        GenComplete();

        ChooseBossRoom();

    }

    private GameObject GetRoom(DoorTypes doorNeeded, int row, int col)
    {
        GameObject room = null;
        List<GameObject> options = new List<GameObject>();

        // Step 1: Check if the Room is on an edge

        Debug.Log(doorNeeded);

        if (row == 0 || row == rowSize - 1 || col == 0 || col == colSize - 1)
        {
            options = GetDoorAmount(1);

            foreach (GameObject r in options)
            {
                RoomTypes rt = r.GetComponent<RoomTypes>();
                if (rt.doors.Contains(doorNeeded))
                {
                    room = r;
                    Debug.Log(doorNeeded);
                    Debug.Log(r.name);
                    break;
                }
            }
        }


        // Step 2: Get Random Room with door type

        else
        {
            if ((chosenRoomCount / 2) + 1 >= currentRoomCount)
            {
                options = GetDoorAmount(2);
            }
            else if ((chosenRoomCount / 3) >= currentRoomCount)
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

        if (roomTypes.Count == 0) 
        {
            roomTypes = GetRoomsByDoorType(dt, GetFullSpawnList(roomList));
        }

        int index = Random.Range(0, roomTypes.Count - 1);

        room = roomTypes.ElementAt(index);


        return room;
    }

    // Code for Shuffle from https://discussions.unity.com/t/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code/535113/2
    private void Shuffle<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    private GameObject GetNewRoom(List<DoorTypes> typesNeeded, List<GameObject> l)
    {
        GameObject room = null;

        Shuffle(l);

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
        else if (sp.doorNeeded == DoorTypes.BOTTOM_DOOR && coord.row >= 1)
        {
            coord.row -= 1;
        }
        else if (sp.doorNeeded == DoorTypes.RIGHT_DOOR && coord.col >= 1)
        {
            coord.col += 1;
        }
        else if (sp.doorNeeded == DoorTypes.LEFT_DOOR && coord.col < colSize - 1)
        {
            coord.col -= 1;
        }

        //Debug.Log("COORD: " + coord.row + " " + coord.col);

        return coord;
    }

    private void ChooseBossRoom()
    {
        GameObject room = roomsPlaced[roomsPlaced.Count - 1];
        RoomTypes rt = room.GetComponent<RoomTypes>();
        List<DoorTypes> dt = rt.doors;

        List<GameObject> emptyRooms = GetFullSpawnList(emptyRoomsList);
        
        foreach (GameObject r in emptyRooms)
        {
            RoomTypes rRt = r.GetComponent<RoomTypes>();

            int count = 0;
            foreach (DoorTypes d in dt)
            {
                if (rRt.doors.Contains(d))
                {
                    count++;
                }
            }
            if (count == rRt.doors.Count)
            {
                roomsPlaced[roomsPlaced.Count - 1] = Instantiate(r, room.transform.position, Quaternion.identity, transform);
                Destroy(room);
                break;
            }
        }

        Debug.Log("room placed");

    }

    #endregion

    #region Room Lists

    private List<GameObject> GetFullSpawnList(RoomList rl)
    {
        List<GameObject> AllDoors = new List<GameObject>();
        AllDoors.AddRange(rl.topRooms);
        AllDoors.AddRange(rl.bottomRooms);
        AllDoors.AddRange(rl.leftRooms);
        AllDoors.AddRange(rl.rightRooms);
        AllDoors.Add(startRoom);

        return AllDoors;
    }

    private List<GameObject> GetDoorAmount(int doorCount)
    {
        List<GameObject> allDoors = GetFullSpawnList(roomList);

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
