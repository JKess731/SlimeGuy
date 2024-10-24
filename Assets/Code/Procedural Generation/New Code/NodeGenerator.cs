using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NodeGenerator : MonoBehaviour
{
    private ProceduralManager manager;
    [SerializeField] private GameObject nodePrefab;

    // Data for node storage -> Gets passed down from ProceduralManager
    private int nodeCount;
    private int arrRow;
    private int arrCol;
    [SerializeField] private float nodeDelay = .1f;
    private RoomNode[,] nodeArray;
    private List<RoomNode> nodesPlaced = new List<RoomNode>();
    private Queue<RoomNode> nodeQueue = new Queue<RoomNode>();

    public UnityEvent OnNodesFinishes;

    #region Properties

    public RoomNode[,] NodeArray { get { return nodeArray; } }
    public List<RoomNode> NodesPlaced { get { return nodesPlaced; } }
    public int NodeCount { get { return nodeCount; } set { nodeCount = value; } }
    public int ArrayRow {  get { return arrRow; } set { arrRow = value; } }
    public int ArrayCol { get { return arrCol; } set { arrCol = value; } }
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
        nodeArray = CreateNodeArray(arrRow, arrCol);
        PlaceFirstNode();
    }

    #region Functions

    // Returns a new RoomNode 2D array to store Node
    private RoomNode[,] CreateNodeArray(int r, int c)
    {
        return new RoomNode[r, c];
    }

    /*
    private ArrayCoordinate ChooseFirstNodeLocation()
    {
        int rowMin = 1;
        int rowMax = arrRow - 1;
        int colMin = 1;
        int colMax = arrCol - 1;

        ArrayCoordinate coord = new ArrayCoordinate();
        coord.row = Random.Range(rowMin, rowMax);
        coord.col = Random.Range(colMin, colMax);
        return coord;
    }
    */

    private void PlaceFirstNode()
    {
        GameObject node = Instantiate(nodePrefab, Vector2.zero, Quaternion.identity);
        RoomNode nodeScript = node.GetComponent<RoomNode>();
        //ArrayCoordinate coord = ChooseFirstNodeLocation();
        int row = arrRow / 2;
        int col = arrCol / 2;
        nodeArray[row, col] = nodeScript;

        int x = 0;
        int y = 0;

        node.transform.position = new Vector2(x, y);
        nodesPlaced.Add(nodeScript);
        nodeCount--;

        GameObject topNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x, node.transform.position.y + 24), Quaternion.identity);
        RoomNode trn = topNode.GetComponent<RoomNode>();
        nodeQueue.Enqueue(trn);
        nodeArray[row - 1, col] = trn;
        nodesPlaced.Add(trn);
        nodeCount--;

        GameObject bottomNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x, node.transform.position.y - 24), Quaternion.identity);
        RoomNode brn = bottomNode.GetComponent<RoomNode>();
        nodeQueue.Enqueue(brn);
        nodeArray[row + 1, col] = brn;
        nodesPlaced.Add(brn);
        nodeCount--;

        GameObject rightNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x + 24, node.transform.position.y), Quaternion.identity);
        RoomNode rrn = rightNode.GetComponent<RoomNode>();
        nodeQueue.Enqueue(rrn);
        nodeArray[row, col + 1] = rrn;
        nodesPlaced.Add(rrn);
        nodeCount--;

        GameObject leftNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x - 24, node.transform.position.y), Quaternion.identity);
        RoomNode lrn = leftNode.GetComponent<RoomNode>();
        nodeQueue.Enqueue(lrn);
        nodeArray[row, col - 1] = lrn;
        nodesPlaced.Add(lrn);
        nodeCount--;

        StartCoroutine(PlaceNextNodes());
    }

    private IEnumerator PlaceNextNodes()
    {
        while (nodeCount > 0)
        {
            yield return new WaitForSeconds(nodeDelay);

            RoomNode node = nodeQueue.Dequeue();
            ArrayCoordinate coord = GetNodeCoordinate(node);
            int row = coord.row;
            int col = coord.col;

            List<int> nodeDirections = CheckDirections(row, col);
            if (nodeDirections.Count > 0)
            {
                PlaceNodeInRandomDiection(nodeDirections, node.gameObject, row, col);
            }
        }

        foreach (RoomNode n in nodesPlaced)
        {
            ArrayCoordinate coord = GetNodeCoordinate(n);
            int r = coord.row;
            int c = coord.col;

            List<int> d = CheckDirectionsNotNull(r, c);
            foreach (int i in d)
            {
                if (i == 1)
                {
                    n.doors.Add(DoorTypes.TOP_DOOR);
                }
                else if (i == 2)
                {
                    n.doors.Add(DoorTypes.BOTTOM_DOOR);
                }
                else if (i == 3)
                {
                    n.doors.Add(DoorTypes.RIGHT_DOOR);
                }
                else if (i == 4)
                {
                    n.doors.Add(DoorTypes.LEFT_DOOR);
                }
            }
        }

        OnNodesFinishes?.Invoke();
    }

    private List<int> CheckDirections(int row, int col)
    {
        List<int> directions = new List<int>();

        if (row - 1 >= 0)
        {
            if (nodeArray[row - 1, col] == null)
            {
                // There can be another node above me.
                directions.Add(1);
            }
        }

        if (row + 1 <= arrRow - 1)
        {
            if (nodeArray[row + 1, col] == null)
            {
                // There can be another node below me
                directions.Add(2);
            }
        }

        if (col + 1 <= arrCol - 1)
        {
            if (nodeArray[row, col + 1] == null)
            {
                // There can be another node right of me
                directions.Add(3);
            }
        }

        if (col - 1 >= 0)
        {
            if (nodeArray[row, col - 1] == null)
            {
                // There can be another node left of me
                directions.Add(4);
            }
        }

        return directions;
    }

    private List<int> CheckDirectionsNotNull(int row, int col)
    {
        List<int> directions = new List<int>();

        if (row - 1 >= 0)
        {
            if (nodeArray[row - 1, col] != null)
            {
                // There can be another node above me.
                directions.Add(1);
            }
        }

        if (row + 1 <= arrRow - 1)
        {
            if (nodeArray[row + 1, col] != null)
            {
                // There can be another node below me
                directions.Add(2);
            }
        }

        if (col + 1 <= arrCol - 1)
        {
            if (nodeArray[row, col + 1] != null)
            {
                // There can be another node right of me
                directions.Add(3);
            }
        }

        if (col - 1 >= 0)
        {
            if (nodeArray[row, col - 1] != null)
            {
                // There can be another node left of me
                directions.Add(4);
            }
        }

        return directions;
    }

    private void PlaceNodeInRandomDiection(List<int> directions, GameObject node, int row, int col)
    {
        int direction = directions[Random.Range(0, directions.Count)];

        if (direction == 1)
        {
            GameObject newNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x, node.transform.position.y + 24), Quaternion.identity);
            RoomNode rn = newNode.GetComponent<RoomNode>();
            nodeQueue.Enqueue(rn);
            nodeArray[row - 1, col] = rn;
            nodesPlaced.Add(rn);
            nodeCount--;
        }
        else if (direction == 2)
        {
            GameObject newNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x, node.transform.position.y - 24), Quaternion.identity);
            RoomNode rn = newNode.GetComponent<RoomNode>();
            nodeQueue.Enqueue(rn);
            nodeArray[row + 1, col] = rn;
            nodesPlaced.Add(rn);
            nodeCount--;
        }
        else if (direction == 3)
        {
            GameObject newNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x + 24, node.transform.position.y), Quaternion.identity);
            RoomNode rn = newNode.GetComponent<RoomNode>();
            nodeQueue.Enqueue(rn);
            nodeArray[row, col + 1] = rn;
            nodesPlaced.Add(rn);
            nodeCount--;
        }
        else if (direction == 4)
        {
            GameObject newNode = Instantiate(nodePrefab, new Vector2(node.transform.position.x - 24, node.transform.position.y), Quaternion.identity);
            RoomNode rn = newNode.GetComponent<RoomNode>();
            nodeQueue.Enqueue(rn);
            nodeArray[row, col - 1] = rn;
            nodesPlaced.Add(rn);
            nodeCount--;
        }
    }
  
    private ArrayCoordinate GetNodeCoordinate(RoomNode node)
    {
        ArrayCoordinate coordinate = new ArrayCoordinate();
        for (int x = 0; x < nodeArray.GetLength(0); x++)
        {
            for (int y = 0; y < nodeArray.GetLength(1); y++)
            {
                if (nodeArray[x, y] == node)
                {
                    coordinate.row = x;
                    coordinate.col = y;
                    break;
                }
            }
        }

        return coordinate;
    }

    private void DestroyNodes()
    {
        Queue<GameObject> nodeDestroy = new Queue<GameObject>();
        for (int i = 0; i < nodesPlaced.Count; i++)
        {
            nodeDestroy.Enqueue(nodesPlaced[i].gameObject);
        }

        while (nodeDestroy.Count > 0)
        {
            Destroy(nodeDestroy.Dequeue());
        }

        nodesPlaced.Clear();
    }

    #endregion
}
