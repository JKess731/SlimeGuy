using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonGeneration : MonoBehaviour
{
    [SerializeField] private GameObject startRoom;

    [SerializeField] private int seed;

    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;
    [SerializeField] private int gridSpaceSize = 16;

    public UnityEvent placeRoom;

    private List<List<GameObject>> map = new List<List<GameObject>>();
    private void Start()
    {
        
        for (int w = 0; w < width; w++)
        {
            List<GameObject> list = new List<GameObject>();
            map.Add(list);
            for (int h = 0; h < height; h++)
            {
                map[w].Add(null);
            }
        }

        PlaceStartingRoom();
    }

    private void Update()
    {
        PlaceRoom();
    }

    private void PlaceStartingRoom()
    {
        int middleWidth = width / 2;
        int middleHeight = height / 2;

        if (seed != 0)
        {
            Random.InitState(seed);
        }

        int startWidth = Random.Range(0, width);
        int startHeight = Random.Range(0, height);

        int gridX = gridSpaceSize * startWidth;
        int gridY = gridSpaceSize * startHeight;

        map[middleWidth][middleHeight] = Instantiate(startRoom, new Vector2(gridX, gridY), Quaternion.identity);

        Debug.Log(Random.seed);
    }

    private void PlaceRoom()
    {
        if (placeRoom.GetPersistentEventCount() > 0)
        {
            placeRoom.Invoke();
            placeRoom.RemoveAllListeners();
        }
    }

}
