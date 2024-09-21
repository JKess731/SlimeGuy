using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChest : MonoBehaviour
{
    private LevelGenerator lg;

    [SerializeField] private GameObject chestPrefab;

    private void Start()
    {
        lg = GetComponent<LevelGenerator>();
    }

    private void Update()
    {
        CheckClearedRooms();
    }

    private void CheckClearedRooms()
    {
        if (lg.clearedChain == 2)
        {
            lg.clearedChain = 0;

            GameObject chest = Instantiate(chestPrefab, lg.lastClearedRoom.transform);
            chest.transform.localPosition = new Vector2(0, 5);
        }
    }
}
