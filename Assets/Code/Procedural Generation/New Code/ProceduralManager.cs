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

    #region Properties

    public List<LevelRoomInfo> RoomsTypesList {  get { return _roomsTypesList; } }
    public NodeGenerator NodeGenerator { get { return _nodeGenerator; } }
    public RoomGenerator RoomGenerator { get { return _roomGenerator; } }

    public RoomList EmptyRooms { get { return emptyRoomList; } }

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

    #endregion
}
