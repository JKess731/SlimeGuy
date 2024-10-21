using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private ProceduralGenerator genScript;
    public List<DoorTypes> doorsNeeded;

    public GameObject parentRoom;

    private void Awake()
    {
        genScript = FindAnyObjectByType<ProceduralGenerator>();
    }

    private void Start()
    {
        parentRoom = transform.parent.gameObject;
        genScript.SpawnPointEnqueue(this);
        //genScript.OnRoomPlaced.AddListener(UpdateDoors);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("room_spawn_point"))
        {
            if (genScript.SpawnPoints.Contains(collision.gameObject.GetComponent<SpawnPoint>()))
            {
                Destroy(gameObject);
            }
        }
    }

    /*
    private void UpdateDoors()
    {
        RoomTypes[,] rooms = genScript.Rooms;

        int row = 0;
        int col = 0;
        for (int r = 0; r < rooms.GetLength(0); r++)
        {
            for (int c = 0; c < rooms.GetLength(1); c++)
            {
                if (rooms[r, c] == parentRoom.GetComponent<RoomTypes>())
                {
                    row = r;
                    col = c;
                }
            }
        }

        // Check right by col + 1
        // Check left by col - 1
        // Check Up by row - 1
        // Check down by row + 1


        if (doorsNeeded.Count == 1)
        {
            if (doorsNeeded.Contains(DoorTypes.TOP_DOOR))
            {

            }
            if (doorsNeeded.Contains(DoorTypes.TOP_DOOR))
            {

            }
        }

        if (col + 1 < rooms.GetLength(1))
        {
            if (rooms[row, col + 1] != null)
            {
                RoomTypes rt = rooms[row, col + 1];
                List<DoorTypes> dn = rt.doors;

                if (dn.Contains(DoorTypes.LEFT_DOOR))
                {
                    if (!doorsNeeded.Contains(DoorTypes.RIGHT_DOOR))
                    {
                        doorsNeeded.Add(DoorTypes.RIGHT_DOOR);
                    }
                }
            }
        }

        if (col - 1 > 0)
        {
            if (rooms[row, col - 1] != null)
            {
                RoomTypes rt = rooms[row, col - 1];
                List<DoorTypes> dn = rt.doors;

                if (dn.Contains(DoorTypes.RIGHT_DOOR))
                {
                    if (!doorsNeeded.Contains(DoorTypes.LEFT_DOOR))
                    {
                        doorsNeeded.Add(DoorTypes.LEFT_DOOR);
                    }
                }
            }
        }

        if (row + 1 < rooms.GetLength(0)) 
        {
            if (rooms[row + 1, col] != null)
            {
                RoomTypes rt = rooms[row + 1, col];
                List<DoorTypes> dn = rt.doors;

                if (dn.Contains(DoorTypes.BOTTOM_DOOR))
                {
                    if (!doorsNeeded.Contains(DoorTypes.TOP_DOOR))
                    {
                        doorsNeeded.Add(DoorTypes.TOP_DOOR);
                    }
                }
            }
        }

        if (row - 1 > 0)
        {
            if (rooms[row - 1, col] != null)
            {
                RoomTypes rt = rooms[row - 1, col];
                List<DoorTypes> dn = rt.doors;

                if (dn.Contains(DoorTypes.TOP_DOOR))
                {
                    if (!doorsNeeded.Contains(DoorTypes.BOTTOM_DOOR))
                    {
                        doorsNeeded.Add(DoorTypes.BOTTOM_DOOR);
                    }
                }
            }
        }

    }
    */
}
