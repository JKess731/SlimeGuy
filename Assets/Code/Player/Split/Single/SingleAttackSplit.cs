using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Original Slime Split Code
 */
public class SingleAttackSplit : MonoBehaviour
{
    private SlimeSplit controller;

    private GameObject minionPrefab;


    private void Awake()
    {
        controller = FindAnyObjectByType<SlimeSplit>();
        minionPrefab = controller.minionPrefab;
    }

    /// <summary>
    /// Generates a minion object
    /// </summary>
    /// <returns></returns>
    public GameObject OnSplit()
    {
        controller.minionsLeft--;
        // Spawn Minion at player location
        GameObject toSpawnMinion = Instantiate(minionPrefab);

        toSpawnMinion.transform.position = controller.minionSpawnPos;

        return toSpawnMinion;
    }
}
