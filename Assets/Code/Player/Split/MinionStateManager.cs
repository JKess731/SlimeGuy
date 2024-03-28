using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jared Kessler
// 3/10/24
public class MinionStateManager : MonoBehaviour
{
    // State of the minion & dependancies
    public MinionStateEnum currentState;
    public SlimeSplit slimeSplitController;
    [HideInInspector] public MinionMove minionsMovement;
    [HideInInspector] public MinionShoot minionShoot;
    
    // Editable Variables
    public float lookDistance = 8f;
    public float moveSpeed = 2f;

    // Enemy
    private GameObject target;
    private List<GameObject> enemyList;
    private bool enemyInDistance = false;

    private void Awake()
    {
        // Set State of the player && get references to dependencies
        currentState = MinionStateEnum.MoveToPlayer;
        minionsMovement = GetComponent<MinionMove>();
        slimeSplitController = GameObject.FindAnyObjectByType<SlimeSplit>();
        minionShoot = GetComponent<MinionShoot>();

        // Grab reference to the enemies in the room if in a room & set the initial state
        if (EnemiesInLevel.instance.currentRoom != null)
        {
            enemyList = EnemiesInLevel.instance.rooms[EnemiesInLevel.instance.currentRoom];
            currentState = MinionStateEnum.MoveToEnemy;
        }
    }

    private void Start()
    {
        // Start the lifetime of the minion on the first frame
        StartCoroutine(MinionLifetime(slimeSplitController.lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case MinionStateEnum.MoveToPlayer:
                minionsMovement.OnMoveToPlayer();
                break;
            case MinionStateEnum.MoveToEnemy:
                minionsMovement.OnMoveToEnemy();
                break;
            case MinionStateEnum.AttackEnemy:
                if (minionShoot.canShoot)
                {
                    OnAttackEnemy();
                }
                break;
        }

    }

    public void OnAttackEnemy()
    {
        StartCoroutine(minionShoot.Shoot());
    }

    public GameObject FindNearestEnemy(List<GameObject> enemies)
    {
        GameObject closestEnemy = null;
        List<GameObject> aliveEnemies = RemoveNullEnemies(enemies, GetNullEnemies(enemies));

        bool targetFound = false;

        foreach (GameObject enemy in aliveEnemies)
        {
            if (!targetFound)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= lookDistance)
                {
                    closestEnemy = enemy;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <
                    Vector3.Distance(transform.position, closestEnemy.transform.position))
                {
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null) enemyInDistance = true;
        else enemyInDistance = false;

        return closestEnemy;
    }

    public List<GameObject> GetNullEnemies(List<GameObject> enemies)
    {
        //Enemies that have been killed that need to be removed from the main list
        List<GameObject> nullEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                nullEnemies.Add(enemy);
            }
        }

        return nullEnemies;
    }

    public List<GameObject> RemoveNullEnemies(List<GameObject> enemies, List<GameObject> nullEnemies)
    {
        foreach (GameObject nullEnemy in nullEnemies)
        {
            if (enemies.Contains(nullEnemy)) 
            {
                enemies.Remove(nullEnemy);
            }
        }

        return enemies;
    }

    private IEnumerator MinionLifetime(float lifetime)
    {
        Debug.Log("SLIME SPLIT: Lifetime started");
        // Wait the lifetime of the minion before doing anything
        yield return new WaitForSeconds(lifetime);

        // Notify the  slime split controller that a minion has despawned
        // run a function in the main slime split that starts the cooldown
        slimeSplitController.StartCooldown();
        Debug.Log("SLIME SPLIT: Lifetime ended");
        Destroy(gameObject);
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public List<GameObject> GetEnemyList()
    {
        return enemyList;
    }

    private void CheckEnemyDistance()
    {
        int count = 0;
        if (enemyList != null && enemyList.Count > 0)
        {
            foreach (GameObject enemy in enemyList)
            {
                if (enemy != null)
                {
                    count++;
                    if (Vector3.Distance(transform.position, enemy.transform.position) <= slimeSplitController.enemyDistance)
                    {
                        enemyInDistance = true;
                        break;
                    }
                }
            }
        }

        if (count == enemyList.Count || enemyList == null)
        {
            enemyInDistance = false;
        }
    }

    public bool IsEnemyInDistance()
    {
        CheckEnemyDistance();
        return enemyInDistance;
    }    
}

public enum MinionStateEnum
{
    MoveToPlayer,
    MoveToEnemy,
    AttackEnemy
}
