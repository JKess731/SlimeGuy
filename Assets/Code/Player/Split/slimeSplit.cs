using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jared Kessler
public class SlimeSplit : MonoBehaviour
{
    [SerializeField] private GameObject minion;

    // List for enemies that are in the room
    [HideInInspector] public List<GameObject> enemiesInRoom = new List<GameObject>();
    [HideInInspector] public int minionCounter = 1;

    // Update is called once per frame
    void Update()
    {
        // Temp Input
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (minionCounter > 0)
            {
                GameObject newMinion = OnSplit();
            }
        }
    }

    /// <summary>
    /// Generates a minion object
    /// </summary>
    /// <returns></returns>
    private GameObject OnSplit()
    {
        minionCounter--;
        // Spawn Minion at player location
        GameObject spawnedMinion = Instantiate(minion);
        spawnedMinion.transform.position = transform.parent.position;

        return spawnedMinion;
    }
}
