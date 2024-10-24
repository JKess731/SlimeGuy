using System;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralManager : MonoBehaviour
{
    // Generators
    [SerializeField] private NodeGenerator _nodeGenerator;
    [SerializeField] private RoomGenerator _roomGenerator;

    [Header("Rooms")]
    [Space]
    [SerializeField] private List<LevelRoomInfo> _roomsTypesList = new List<LevelRoomInfo>();

    // Array Storage Info
    [Space]
    [Header("Settings")]
    [SerializeField] private float _spawnDelay = .25f;
    [SerializeField] private int _rowSize = 10;
    [SerializeField] private int _colSize = 10;

    // Room Lists
    //[SerializeField] private GameObject startRoom;
    [SerializeField] private RoomList emptyRoomList;
    [SerializeField] private RoomList eventRoomList;
    [SerializeField] private RoomList waveRoomList;
    [SerializeField] private RoomList basicRoomList;
    [SerializeField] private RoomList bossRoomList;
    [SerializeField] private RoomList shopRoomList;
    private Queue<RoomTag> tagQueue = new Queue<RoomTag>();

    #region Properties

    public List<LevelRoomInfo> RoomsTypesList { get { return _roomsTypesList; } }
    public NodeGenerator NodeGenerator { get { return _nodeGenerator; } }
    public RoomGenerator RoomGenerator { get { return _roomGenerator; } }
    public Queue<RoomTag> TagQueue { get { return tagQueue; } }

    public RoomList EmptyRooms { get { return emptyRoomList; } }
    public RoomList BasicRooms {  get {  return basicRoomList; } }

    #endregion

    #region Classes
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

    #endregion

    private void Start()
    {
        InitGenerators();
        AudioManager.instance.PlayIValley();
        AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 0);
        AudioManager.instance.IValleyTheme.setParameterByName("enemyNear", 1f, false);
        float value1 = 1234567f;
        AudioManager.instance.IValleyTheme.getParameterByName("enemyNear",out value1);
        Debug.Log(value1);
    }

    #region Functions

    private void InitGenerators()
    {
        // Initializes both Node & Room Generators with Needed data

        _nodeGenerator.Manager = this;
        _nodeGenerator.ArrayRow = _rowSize;
        _nodeGenerator.ArrayCol = _colSize;

        // Tell the Node Generator how many nodes it needs to spawn
        _nodeGenerator.NodeCount = GetRoomTotalCount();

        // ----------------------------------
        _roomGenerator.Manager = this;
        _roomGenerator.ArrayRow = _rowSize;
        _roomGenerator.ArrayCol = _colSize;
        _roomGenerator.SpawnDelay = _spawnDelay;
        _roomGenerator.RoomCount = GetRoomTotalCount();

        // Lets the room generator grab a reference to the node generator from here
        _roomGenerator.InitRoomGenerator();
        QueueTags(_roomsTypesList);
    }

    private int GetRoomTotalCount()
    {
        int i = 0;
        foreach (LevelRoomInfo lri in _roomsTypesList)
        {
            i += lri.count;
        }
        return i;
    }

    private void QueueTags(List<LevelRoomInfo> list)
    {
        List<RoomTag> newList = new List<RoomTag>();

        foreach (LevelRoomInfo i in list)
        {
            if (i.rt != RoomTag.EMPTY)
            {
                for (int n = i.count; n > 0; n--)
                {
                    newList.Add(i.rt);
                }
            }
        }

        Shuffle(newList);

        newList.Insert(0, RoomTag.EMPTY);

        if (newList.Contains(RoomTag.BOSS))
        {
            newList.Remove(RoomTag.BOSS);
        }

        newList.Add(RoomTag.BOSS);

        foreach (RoomTag t in newList)
        {
            tagQueue.Enqueue(t);
        }
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
