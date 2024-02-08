using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jared Kessler
public class slimeSplit : MonoBehaviour
{

    [SerializeField] private GameObject minion;

    // Update is called once per frame
    void Update()
    {
        // Temp Input
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            GameObject newMinion = onSplit();
            MinionMove minionObj = newMinion.GetComponent<MinionMove>();

            // Temp find "closest" enemy
            /*
             * Will want to change this later when more than 1
             * enemy is on the screen at a time
             */
            GameObject enemy = GameObject.FindWithTag("enemy");

            minionObj.target = enemy;

        }
    }

    private GameObject onSplit()
    {
        // Spawn Minion at player location
        GameObject spawnedMinion = Instantiate(minion);
        spawnedMinion.transform.position = transform.position;

        return spawnedMinion;
    }
}
