using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, IMinionDamageable, IMinionMoveable
{
    [field: SerializeField] public float maxHealth { get; set; } = 100f;
    public float health { get; set; }
    public Rigidbody2D rb { get; set; }
    public bool isMovingToEnemy { get; set; } = false;
    public float lookDistance { get; set; } = 8f;

    #region State Variables

    public MinionStateMachine stateMachine { get; set; }
    public MinionToPlayerState toPlayerState { get; set; }
    public MinionToEnemyState toEnemyState { get; set; }
    public MinionIdleState idleState { get; set; }
    public MinionAttackState attackState { get; set; }

    #endregion

    void Awake()
    {
        stateMachine = new MinionStateMachine();

        toPlayerState = new MinionToPlayerState(this, stateMachine);
        toEnemyState = new MinionToEnemyState(this, stateMachine);

    }

    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(toPlayerState);
        Debug.Log("In player State ");
        Debug.Log(stateMachine.currentState);
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();

        if (stateMachine.currentState == attackState)
        {
            Debug.Log("Shoot");
            StartCoroutine(attackState.Shoot(2f));
        }
    }

    #region Health & Damage Functions
    public void Damage(float dmg)
    {
        
    }

    public void Die()
    {

    }

    #endregion

    #region Movement Functions
    public void MoveMinion(Vector2 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, 2 * Time.deltaTime);
    }

    #region Find Nearest Enemy

    public GameObject FindNearestEnemy(List<GameObject> enemyList)
    {
        GameObject closestEnemy = null;
        List<GameObject> aliveEnemies = RemoveNullEnemies(enemyList, GetNullEnemies(enemyList));

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

    #endregion

    #endregion

    #region Animation Events

    public void AnimationTriggerEvent(AnimationTriggerType a)
    {

    }

    public enum AnimationTriggerType
    {
        MinionDamaged
    }

    #endregion
}


