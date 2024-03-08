using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInLevel : MonoBehaviour
{
    public static EnemiesInLevel instance { get; private set; }

    public Dictionary<GameObject,List<GameObject>> rooms = new Dictionary<GameObject,List<GameObject>>();
    public GameObject currentRoom;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public List<GameObject> GetEnemies(GameObject room)
    {
        List<GameObject> enemies = new List<GameObject>();

        if (rooms.ContainsKey(room))
        {
            enemies = rooms[room];
        }

        return enemies;
    }

    public void ResetCurrentRoom()
    {
        List<GameObject> currentRoomList = rooms[currentRoom];

        int count = 0;
        foreach(GameObject enemy in currentRoomList)
        {
            if (enemy == null)
            {
                count++;
            }
        }

        if (count == currentRoomList.Count)
        {
            currentRoom = null;
            rooms[currentRoom].Clear();
        }
    }
}
