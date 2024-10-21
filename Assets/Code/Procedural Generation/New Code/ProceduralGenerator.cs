using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private RoomList shopRoomList;

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
    [SerializeField] private List<GameObject> roomsPlaced = new List<GameObject>();

    private Queue<SpawnPoint> spawnPoints = new Queue<SpawnPoint>();
    private Queue<RoomTag> roomTags = new Queue<RoomTag>();

    //---------------------------------------------------------------

    // Events
    public UnityEvent OnRoomPlaced;

    //---------------------------------------------------------------

    // Properties

    public RoomTypes[,] Rooms { get { return rooms; } }
    public List<GameObject> RoomsPlaced { get { return roomsPlaced; } }
    public Queue<SpawnPoint> SpawnPoints {  get { return spawnPoints; } }

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
        OnRoomPlaced?.Invoke();
        RoomTypes rt = start.GetComponent<RoomTypes>();
        rooms[middleX, middleY] = rt;
        roomsPlaced.Add(start);

        StartCoroutine(Generate());
    }

    #endregion

    #region Generator

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(spawnDelay);

        int spawnCount = roomTags.Count;

        while (spawnCount > 0)
        {
            // Spawn Delay so that the spawnCount Queue can update with new spawn points in enough time
            yield return new WaitForSeconds(spawnDelay);

            // Grab a spawn point reference & find it's coordinates within the array
            SpawnPoint sp = SpawnPointDequeue();
            ArrayCoordinate arrCoords = GetSpawnPointArrayCoords(sp);
            int row = arrCoords.row;
            int col = arrCoords.col;

            List<DoorTypes> doorsNeeded = sp.doorsNeeded;
            //DoorTypes doorNeeded = sp.doorNeeded;

            Debug.Log(spawnCount);

            if (rooms[row, col] == null)
            {
                RoomTag tag = RoomTagDequeue();
                RoomList rList = GetRoomTagList(tag);

                GameObject room = GetRandomRoomFromList(GetRoomsByDoorTypes(doorsNeeded, rList));
                PlaceRoom(room, sp, row, col);
                spawnCount--;
                Destroy(sp.gameObject);
            }
        }
    }


    private RoomList GetRoomTagList(RoomTag tag)
    {

        switch (tag)
        {
            case RoomTag.EVENT:
                return eventRoomList;
            case RoomTag.EMPTY:
                return emptyRoomList;
            case RoomTag.BASIC:
                return basicRoomList;
            case RoomTag.WAVE:
                return waveRoomList;
            case RoomTag.BOSS:
                return bossRoomList;
            case RoomTag.SHOP:
                return shopRoomList;
        }

        return null;
    }

    private void PlaceRoom(GameObject room, SpawnPoint sp, int row, int col)
    {
        GameObject spawnedRoom = Instantiate(room, sp.transform.position, Quaternion.identity, transform);
        OnRoomPlaced?.Invoke();
        rooms[row, col] = spawnedRoom.GetComponent<RoomTypes>();
        roomsPlaced.Add(spawnedRoom);
    }

    private void ReplaceRoom(GameObject oldRoom, GameObject room, int row, int col)
    {
        roomsPlaced.Remove(oldRoom);
        GameObject spawnedRoom = Instantiate(room, oldRoom.transform.position, Quaternion.identity, transform);
        OnRoomPlaced?.Invoke();
        rooms[row, col] = spawnedRoom.GetComponent<RoomTypes>();
        roomsPlaced.Add(spawnedRoom);
        Destroy(oldRoom);
    }

    private GameObject GetRandomRoomFromList(List<GameObject> l)
    {
        return l[UnityEngine.Random.Range(0, l.Count)];
    }

    private List<GameObject> GetRoomsByDoorTypes(List<DoorTypes> listD, RoomList r)
    {
        List<GameObject> options = new List<GameObject>();

        foreach (GameObject room in r.rooms) // Loop through every room in the given RoomList
        {
            // Grab a reference to that rooms RoomTypes object so we can use the rooms List of Doors
            RoomTypes rt = room.GetComponent<RoomTypes>();

            // Loop through all the doors to make sure that the room we choose at least has all the doors this room needs
            int count = 0;
            foreach (DoorTypes d in listD)
            {
                if (rt.doors.Contains(d))
                {
                    count++;
                }
            }

            // Add the door to the options if necessary
            if (count == listD.Count)
            {
                options.Add(room);
            }

        }

        return options;
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

        // Check the Edge Cases & handle them
        if (sp.doorsNeeded.Contains(DoorTypes.TOP_DOOR) && coord.row > 0)
        {
            coord.row -= 1;
        }
        
        if (sp.doorsNeeded.Contains(DoorTypes.BOTTOM_DOOR) && coord.row < rowSize - 1)
        {
            coord.row += 1;
        }
        
        if (sp.doorsNeeded.Contains(DoorTypes.RIGHT_DOOR) && coord.col < colSize - 1)
        {
            coord.col += 1;
        }
        
        if (sp.doorsNeeded.Contains(DoorTypes.LEFT_DOOR) && coord.col > 0)
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

    private RoomTag RoomTagDequeue()
    {
        return roomTags.Dequeue();
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
