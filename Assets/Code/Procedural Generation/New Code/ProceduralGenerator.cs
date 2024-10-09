using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class ProceduralGenerator : MonoBehaviour
{
    //---------------------------------------------------------------

    [Header("Rooms")]
    [SerializeField] private GameObject startRoom;
    [SerializeField] private List<LevelRoomInfo> roomsList = new List<LevelRoomInfo>();
    [SerializeField] private RoomList emptyRoomList;
    [SerializeField] private RoomList eventRoomList;
    [SerializeField] private RoomList waveRoomList;
    [SerializeField] private RoomList basicRoomList;
    [SerializeField] private RoomList bossRoomList;
    //[SerializeField] private RoomList shopRoomList; <--- Will use once we have Shops

    //---------------------------------------------------------------

    // data & settings
    [Space]
    [Header("Array Bounds")]
    [SerializeField] private int rowSize = 5;
    [SerializeField] private int colSize = 5;
    [Space]

    [Header("Settings")]
    [SerializeField] private float spawnDelay = .25f;

    //---------------------------------------------------------------

    // Collections & data
    private RoomTypes[,] rooms;
    private List<GameObject> roomsPlaced = new List<GameObject>();

    private Queue<SpawnPoint> spawnPoints = new Queue<SpawnPoint>();
    private Queue<RoomTag> roomTags = new Queue<RoomTag>();

    //---------------------------------------------------------------

    // Properties

    public RoomTypes[,] Rooms { get { return rooms; } }
    public List<GameObject> RoomsPlaced { get { return roomsPlaced; } }

    //---------------------------------------------------------------

    // Classes

    private class ArrayCoordinate
    {
        public int row = 0;
        public int col = 0;
    }

    [Serializable]
    public class LevelRoomInfo
    {
        public RoomTag rt;
        public int count;
    }

    //---------------------------------------------------------------
    private void Start()
    {
        // Generate array grid
        rooms = GenerateGrid();
        RoomTagEnqueue(CreateRoomOrder(roomsList));
        PlaceStartRoom();
    }

    #region Starter Code

    // Generates a new array to store RoomTypes data for each room in a level
    private RoomTypes[,] GenerateGrid()
    {
        return new RoomTypes[rowSize, colSize];
    }

    // Takes the roominfo's and puts them into a new List and Shuffles them
    private List<RoomTag> CreateRoomOrder(List<LevelRoomInfo> list)
    {
        List<RoomTag> newList = new List<RoomTag>();

        foreach (LevelRoomInfo i in list)
        {
            for (int n = i.count; n > 0; n--)
            {
                newList.Add(i.rt);
            }
        }

        Shuffle(newList);

        if (newList.Contains(RoomTag.BOSS))
        {
            newList.Remove(RoomTag.BOSS);
        }

        newList.Add(RoomTag.BOSS);
        return newList;
        
    }

    private void RoomTagEnqueue(List<RoomTag> list)
    {
        foreach (RoomTag roomTag in list)
        {
            Debug.Log(roomTag);
            roomTags.Enqueue(roomTag);
        }

        Debug.Log("Total Rooms: " + list.Count);
    }

    // Places the starting room at the center of the array & at 0,0 in the scene
    private void PlaceStartRoom()
    {
        int middleX = rowSize / 2;
        int middleY = colSize / 2;

        GameObject start = Instantiate(startRoom, new Vector2(0, 0), Quaternion.identity, transform);
        RoomTypes rt = start.GetComponent<RoomTypes>();
        rooms[middleX, middleY] = rt;
        roomsPlaced.Add(start);

        StartCoroutine(GenerateFirstRooms());
    }

    #endregion

    #region Generator

    private IEnumerator GenerateFirstRooms()
    {
        yield return new WaitForSeconds(spawnDelay);

        int spawnCount = roomTags.Count;

        while (spawnCount > 0)
        {
            // Spawn Delay so that the spawnCount Queue can update with new spawn points in enough time
            yield return new WaitForSecondsRealtime(spawnDelay);

            // Grab a spawn point reference & find it's coordinates within the array
            SpawnPoint sp = SpawnPointDequeue();
            ArrayCoordinate arrCoords = GetSpawnPointArrayCoords(sp);
            int row = arrCoords.row;
            int col = arrCoords.col;
            DoorTypes doorNeeded = sp.doorNeeded;

            if ()

            if (rooms[row, col] == null)
            {
                // Get all random empty rooms with 2 doors or 3 doors & pick one randomly to spawn


                GameObject room = GetRandomEmptyRoomDoorType(doorNeeded);
                GameObject spawnedRoom = Instantiate(room, sp.transform.position, Quaternion.identity, transform);
                rooms[row, col] = spawnedRoom.GetComponent<RoomTypes>();
                roomsPlaced.Add(spawnedRoom);
                spawnCount--;
            }
            else
            {
                Destroy(sp);
            }
        }

        foreach (SpawnPoint s in spawnPoints)
        {
            Destroy(s);
        }
        spawnPoints.Clear();
    }

    /// <summary>
    /// Returns a random empty room with DoorType d
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private GameObject GetRandomEmptyRoomDoorType(DoorTypes d)
    {
        List<GameObject> firstList = GetRoomsByDoorCount(2, emptyRoomList);
        firstList.AddRange(GetRoomsByDoorCount(3, emptyRoomList));

        List<GameObject> emptyRoomsD = GetRoomsByDoorType(d, firstList);
        GameObject g = emptyRoomsD[UnityEngine.Random.Range(0, emptyRoomsD.Count)];
        return g;
    }

    /// <summary>
    /// Returns all rooms containing DoorTypes d in the given RoomList r
    /// </summary>
    /// <param name="d"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    private List<GameObject> GetRoomsByDoorType(DoorTypes d, List<GameObject> r)
    {
        List<GameObject> rooms = new List<GameObject>();

        foreach (GameObject room in r)
        {
            RoomTypes rt = room.GetComponent<RoomTypes>();
            if (rt.doors.Contains(d)) rooms.Add(room);
        }

        return rooms;
    }

    /// <summary>
    /// Returns all rooms containing all DoorTypes in listD from the given RoomList r
    /// </summary>
    /// <param name="listD"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    private List<GameObject> GetRoomsByDoorTypes(List<DoorTypes> listD, RoomList r)
    {
        List<GameObject> rooms = new List<GameObject>();

        foreach (GameObject room in r.rooms)
        {
            RoomTypes rt = room.GetComponent<RoomTypes>();
            
            int counter = 0;
            foreach (DoorTypes d in rt.doors)
            {
                if (listD.Contains(d))
                {
                    counter++;
                }
            }

            if (counter == listD.Count) rooms.Add(room);

        }

        return rooms;
    }

    /// <summary>
    /// Returns all rooms with a Door Count of count from the given RoomList r
    /// </summary>
    /// <param name="count"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    private List<GameObject> GetRoomsByDoorCount(int count, RoomList r)
    {
        List<GameObject> rooms = new List<GameObject>();

        foreach (GameObject room in r.rooms)
        {
            RoomTypes rt = room.GetComponent<RoomTypes>();
            if (rt.doors.Count == count)
            {
                rooms.Add(room);
            }
        }

        return rooms;
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(spawnDelay);

        // Loop through all the spoints in the queue & spawn a random room at it's position
        while (spawnPoints.Count > 0)
        {
            yield return new WaitForSecondsRealtime(spawnDelay);

            SpawnPoint sp = SpawnPointDequeue();
            ArrayCoordinate arrCoords = GetSpawnPointArrayCoords(sp);
            int row = arrCoords.row;
            int col = arrCoords.col;
            DoorTypes doorNeeded = sp.doorNeeded;

            if (rooms[row, col] != null)
            {

            }
        }
    }


    // REturns the Array Location of the given spawnpoint in relation to it's parent room
    // & where it is spawning a room
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

        // Check the Edge Cases & handle them
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

        return coord;
    }

    public void SpawnPointEnqueue(SpawnPoint sp)
    {
        spawnPoints.Enqueue(sp);
    }

    private SpawnPoint SpawnPointDequeue()
    {
        SpawnPoint sp = spawnPoints.Dequeue();

        // If the spawnpoint has no parent room, skip it
        if (sp.parentRoom == null)
        {
            return SpawnPointDequeue();
        }

        return sp;
    }

    #endregion

    #region Utility

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

    #endregion
}
