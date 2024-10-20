using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private ProceduralManager manager;
    private NodeGenerator nodeGenerator;

    // Data for room storage -> Gets passed down from ProceduralManager
    private int roomCount;
    private int arrRow;
    private int arrCol;
    private float spawnDelay;
    private GameObject[,] roomArray;
    private RoomNode[,] nodeArray;

    private RoomList emptyRoomList;

    #region Properties
    public int RoomCount { get { return roomCount; } set { roomCount = value; } }
    public int ArrayRow { get { return arrRow; } set { arrRow = value; } }
    public int ArrayCol { get { return arrCol; } set { arrCol = value; } }
    public float SpawnDelay {  get { return spawnDelay; } set { spawnDelay = value; } }
    public ProceduralManager Manager { get { return manager; } set { manager = value; } }

    #endregion

    #region Classes
    private class ArrayCoordinate
    {
        public int row = 0;
        public int col = 0;
    }

    #endregion

    private void Start()
    {
        roomArray = CreateRoomArray(arrRow, arrCol);
    }

    #region Functions

    // Returns a new RoomNode 2D array to store Node
    private GameObject[,] CreateRoomArray(int r, int c)
    {
        return new GameObject[r, c];
    }

    public void InitRoomGenerator()
    {
        nodeGenerator = manager.NodeGenerator;
        nodeGenerator.OnNodesFinishes.AddListener(StartRoomGeneration);
    }

    private void StartRoomGeneration()
    {
        // Get Values needed after Nodes are finished generator
        nodeArray = nodeGenerator.NodeArray;
        emptyRoomList = manager.EmptyRooms;

        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        for (int i = 0; i < nodeGenerator.NodesPlaced.Count; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            // Get a reference to the current node
            RoomNode node = nodeGenerator.NodesPlaced[i];
            Debug.Log(i);
            for (int j = 0; j < node.doors.Count; j++)
            {
                Debug.Log(node.doors[j]);
            }
            GameObject nodeObj = node.gameObject;
            ArrayCoordinate nodeCoords = GetNodeCoord(node);
            int row = nodeCoords.row;
            int col = nodeCoords.col;

            // Create a list of doors based on node's neighbors
            List<DoorTypes> doorsNeeded = new List<DoorTypes>();
            doorsNeeded = GetNodeNeighbors(node, nodeCoords);
            GameObject room = GetRoomByDoorsNeeded(doorsNeeded, emptyRoomList);
            GameObject spawnedRoom = Instantiate(room, nodeObj.transform.position, Quaternion.identity, transform);

            roomArray[row, col] = spawnedRoom;
        }           
    }

    private List<DoorTypes> GetNodeNeighbors(RoomNode node, ArrayCoordinate nodeCoord)
    {
        List<DoorTypes> doorsNeeded = node.doors;
        
        /*
        int row = nodeCoord.row;
        int col = nodeCoord.col;
        /*
        if (row > 0)
        {
            if (nodeArray[row - 1, col] != null)
            {
                doorsNeeded.Add(DoorTypes.TOP_DOOR);
            }
        }

        if (row + 1 < nodeArray.GetLength(0))
        {
            if (nodeArray[row + 1, col] != null)
            {
                doorsNeeded.Add(DoorTypes.BOTTOM_DOOR);
            }
        }

        if (col + 1 < nodeArray.GetLength(1))
        {
            if (nodeArray[row, col + 1] != null)
            {
                doorsNeeded.Add(DoorTypes.RIGHT_DOOR);
            }
        }

        if (col > 0)
        {
            if (nodeArray[row, col - 1] != null)
            {
                doorsNeeded.Add(DoorTypes.LEFT_DOOR);
            }
        }*/

        return doorsNeeded;
    }

    private ArrayCoordinate GetNodeCoord(RoomNode node)
    {
        ArrayCoordinate coord = new ArrayCoordinate();

        for (int r = 0; r < nodeGenerator.NodeArray.GetLength(0); r++)
        {
            for (int c = 0; c < nodeGenerator.NodeArray.GetLength(1); c++)
            {
                if (nodeGenerator.NodeArray[r,c] == node)
                {
                    coord.row = r;
                    coord.col = c;
                }
            }
        }

        return coord;
    }

    private GameObject GetRoomByDoorsNeeded(List<DoorTypes> doorsNeeded, RoomList list)
    {
        List<GameObject> byCount = new List<GameObject>();
        Debug.Log("TAKES: " + doorsNeeded.Count);

        foreach (GameObject r in list.rooms)
        {
            RoomTypes rt = r.GetComponent<RoomTypes>();
            if (rt.doors.Count == doorsNeeded.Count)
            {
                byCount.Add(r);
            }
        }

        List<GameObject> options = new List<GameObject>();

        foreach (GameObject r in byCount)
        {
            RoomTypes rt = r.GetComponent<RoomTypes>();
            int c = 0;

            foreach (DoorTypes d in doorsNeeded)
            {
                if (rt.doors.Contains(d))
                {
                    c++;
                }
            }
            if (c == doorsNeeded.Count)
            {
                options.Add(r);
            }
        }
        GameObject result = options[Random.Range(0, options.Count)];
        Debug.Log("GIVES: " + result.name);
        return result;
    }

    #endregion
}
