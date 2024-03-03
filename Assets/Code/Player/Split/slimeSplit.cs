using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Jared Kessler
public class SlimeSplit : MonoBehaviour
{
    [SerializeField] private GameObject minionPrefab;

    // List for enemies that are in the room
    [HideInInspector] public List<GameObject> enemiesInRoom = new List<GameObject>();
    [HideInInspector] public int minionCounter = 1;
    [HideInInspector] public int enemyDistance = 8;
    [HideInInspector] public bool enemyFound = false;

    private LookAtEnemy lookAtEnemy;
    private Vector3 minionSpawnPos;

    private void Awake()
    {
        lookAtEnemy = transform.GetChild(0).gameObject.GetComponent<LookAtEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        minionSpawnPos = lookAtEnemy.transform.GetChild(0).position;

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
        GameObject toSpawnMinion = Instantiate(minionPrefab);

        toSpawnMinion.transform.position = minionSpawnPos; 

        return toSpawnMinion;
    }
}
