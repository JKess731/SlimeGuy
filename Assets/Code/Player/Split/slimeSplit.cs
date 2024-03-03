using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Jared Kessler
public class SlimeSplit : MonoBehaviour
{
    [SerializeField] private GameObject minionPrefab;
    public float cooldownTime = 3f;
    public float lifetime = 12f;

    // List for enemies that are in the room
    [HideInInspector] public List<GameObject> enemiesInRoom = new List<GameObject>();
    [HideInInspector] public int minionCounter = 1;
    [HideInInspector] public int enemyDistance = 8;
    [HideInInspector] public bool enemyFound = false;

    private LookAtEnemy lookAtEnemy;
    private Vector3 minionSpawnPos;
    private int minionsLeft;

    private void Awake()
    {
        lookAtEnemy = transform.GetChild(0).gameObject.GetComponent<LookAtEnemy>();
        minionsLeft = minionCounter;
    }

    // Update is called once per frame
    void Update()
    {
        minionSpawnPos = lookAtEnemy.transform.GetChild(0).position;

        // Temp Input
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (minionsLeft > 0)
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
        minionsLeft--;
        // Spawn Minion at player location
        GameObject toSpawnMinion = Instantiate(minionPrefab);

        toSpawnMinion.transform.position = minionSpawnPos; 

        return toSpawnMinion;
    }

    public void StartCooldown()
    {
        StartCoroutine(Cooldown(cooldownTime));
    }

    private IEnumerator Cooldown(float coolTime)
    {
        Debug.Log("SLIME SPLIT: Cooldown started, " + minionsLeft + " minions can be spawned");
        yield return new WaitForSeconds(coolTime);
        minionsLeft++;
        Debug.Log("Cooldown ended, " + minionsLeft + " minions can be spawned");
    }
}
