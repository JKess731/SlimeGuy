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

            // Temp find "closest" enemy
            /*
             * Will want to change this later when more than 1
             * enemy is on the screen at a time
             */
            GameObject enemy = GameObject.FindWithTag("enemy");

            // Temp move enemy
            /*
             * Currently, move to the found enemy for testing
             * 
             * Will want to change this later to:
             * If no enemy in range, move back to player
             * otherwise, move to enemy
             */

            if (Vector3.Distance(newMinion.transform.position, enemy.transform.position) > 1)
            {
                newMinion.transform.position = Vector3.MoveTowards(newMinion.transform.position, enemy.transform.position, 5 * Time.deltaTime);
            }

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
