using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMove : MonoBehaviour
{

    public GameObject target;

    void Update()
    {
        // Temp move enemy
        /*
         * Currently, move to the found enemy for testing
         * 
         * Will want to change this later to:
         * If no enemy in range, move back to player
         * otherwise, move to enemy
         */

        if (Vector3.Distance(transform.position, target.transform.position) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 5 * Time.deltaTime);
        }
    }
}
