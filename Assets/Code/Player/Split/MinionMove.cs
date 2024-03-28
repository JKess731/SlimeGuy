using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Jared Kessler
// 2/15/24 1:59 PM
public class MinionMove : MonoBehaviour
{
    // Player dependendencies
    private GameObject player;
    private SlimeSplit slimeSplitControler;

    // State Manager References
    private MinionStateManager stateManager;
    private GameObject target;
    private List<GameObject> enemies;

    // lookAtEnemy is the script on the child of the SlimeSplit game object
    private LookAtEnemy lookAtEnemy;


    private void Awake()
    {
        // Set player component references
        player = GameObject.FindWithTag("player");
        slimeSplitControler = GameObject.FindWithTag("slime_split").GetComponent<SlimeSplit>();

        // State Control references
        stateManager = GetComponent<MinionStateManager>();
        target = stateManager.GetTarget();
        enemies = stateManager.GetEnemyList();
    }

    public void Update()
    {
        enemies = stateManager.GetEnemyList();
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < slimeSplitControler.minionStopDistanceFromEnemy)
            {
                stateManager.currentState = MinionStateEnum.AttackEnemy;
            }
        }
    }

    public void OnMoveToPlayer()
    {
        if (target == null)
        {
            target = stateManager.FindNearestEnemy(enemies);

            if (target != null) stateManager.currentState = MinionStateEnum.MoveToEnemy;
            else
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, stateManager.moveSpeed * Time.deltaTime);
            }
        }
    }

    public void OnMoveToEnemy()
    {
        // No enemies & no target, switch state
        if (enemies.Count <= 0)
        {
            stateManager.currentState = MinionStateEnum.MoveToPlayer;
        }

        // If there are enemies and no target has been selected, pick a target
        if (enemies.Count > 0 && target == null)
        {
            target = stateManager.FindNearestEnemy(enemies);
        }

        // If there is a target, move to it
        if (enemies.Count > 0 && target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, stateManager.moveSpeed * Time.deltaTime);
        }

    }
}
