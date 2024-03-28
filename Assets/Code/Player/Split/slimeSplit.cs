using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Jared Kessler
public class SlimeSplit : MonoBehaviour
{
    // Public variables that are editable
    public GameObject minionPrefab;
    public float cooldownTime = 3f;
    public float lifetime = 8f;
    public bool dashSplit = true;
    public float minionStopDistanceFromEnemy = 3f;
    public float shootDelay = .3f;

    // Public variables that get edited with the Custom Editor
    [HideInInspector] public int minionCounter = 1;
    [HideInInspector] public float enemyDistance = 8f;

    // Public variables that don't want cluttering the inspector
    // they talk between themselves and other scripts
    [HideInInspector] public List<GameObject> enemiesInRoom = new List<GameObject>();
    [HideInInspector] public bool enemyFound = false;
    [HideInInspector] public int minionsLeft;
    [HideInInspector] public Vector3 minionSpawnPos;

    // Private variables
    private LookAtEnemy lookAtEnemy;
    private SingleAttackSplit singleAttack;
    private DashSplit dashAttack;

    private GameObject currentRoom = null;

    private void Awake()
    {
        lookAtEnemy = transform.GetChild(0).GetComponent<LookAtEnemy>();
        singleAttack = transform.GetChild(1).GetComponent<SingleAttackSplit>();
        dashAttack = transform.GetChild(2).GetComponent<DashSplit>();
        minionsLeft = minionCounter;

        currentRoom = EnemiesInLevel.instance.currentRoom;

        if (currentRoom != null)
        {
            enemiesInRoom = EnemiesInLevel.instance.GetEnemies(currentRoom);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If in a room, grab the list of enemies from that room
        if (currentRoom != null)
        {
            enemiesInRoom = EnemiesInLevel.instance.GetEnemies(currentRoom);
        }
        else
        {
            // Otherwise check if in a room and set currentRoom to either null again
            // or to the room the player is in
            currentRoom = EnemiesInLevel.instance.currentRoom;
        }

        // If not looking at an enemy and there are enemies in the room, set the closest enemy
        // to be the first enemy in the list in the room
        //if (lookAtEnemy.closestEnemy == null && enemiesInRoom.Count > 0)
        //{
        //    lookAtEnemy.closestEnemy = enemiesInRoom[0].transform;
        //}

        // Set the minions spawn position to the spawn position empty game object
        minionSpawnPos = lookAtEnemy.transform.GetChild(0).position;

        //-----------------------------------------------------------

        // Temp Input
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            // Single Attack Split
            if (!dashSplit)
            {
                if (minionsLeft > 0)
                {
                    singleAttack.OnSplit();
                }
            }
            // Dash Split
            else
            {
                if (minionsLeft > 0)
                {
                    dashAttack.OnSplit();
                    minionsLeft--;
                }
                else
                {
                    StartCoroutine(dashAttack.Dash());
                }
            }
        }
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
