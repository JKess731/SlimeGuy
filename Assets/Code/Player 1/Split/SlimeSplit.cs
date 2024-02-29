using System.Collections.Generic;
using UnityEngine;

// Jared Kessler
public class SlimeSplit : MonoBehaviour
{
    [SerializeField] private int minionCounter = 1;
    [SerializeField] private GameObject minion;

    public List<GameObject> enemiesInRoom = new List<GameObject>();

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
        spawnedMinion.transform.position = transform.position;

        return spawnedMinion;
    }
}
