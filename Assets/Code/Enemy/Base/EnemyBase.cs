using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool isFacingRight { get; set; } = true;

    #region State Machine Variables
    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemySpawningState spawnState { get; set; }
    public EnemyDeathState deathState { get; set; }
    #endregion

    #region Scriptable Objects Variables
    [SerializeField] private EnemyIdleSOBase enemyIdleBase;
    [SerializeField] private EnemyChaseSOBase enemyChaseBase;
    [SerializeField] private EnemyAttackSOBase enemyAttackBase;
    [SerializeField] private EnemySpawnSOBase enemySpawnBase;
    [SerializeField] private EnemyDeathSOBase enemyDeathBase;

    public EnemyIdleSOBase enemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase enemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase enemyAttackBaseInstance { get; set; }
    public EnemySpawnSOBase enemySpawnBaseInstance { get; set; }
    public EnemyDeathSOBase enemyDeathBaseInstance { get; set; }

    #endregion

    #region Idle Variables
    #endregion

    #region Trigger Variables
    public bool isAggroed { get; set; }
    public bool isWithinStikingDistance { get; set; }
    #endregion

    public Animator animator;

    private void Awake()
    {
        enemyIdleBaseInstance = Instantiate(enemyIdleBase);
        enemyChaseBaseInstance = Instantiate(enemyChaseBase);
        enemyAttackBaseInstance = Instantiate(enemyAttackBase);
        enemySpawnBaseInstance = Instantiate(enemySpawnBase);
        enemyDeathBaseInstance = Instantiate(enemyDeathBase);

        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        spawnState = new EnemySpawningState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);
    }

    private void Start()
    {

        currentHealth = maxHealth;
        RB = GetComponent<Rigidbody2D>();

        enemyIdleBaseInstance.Initialize(gameObject, this);
        enemyChaseBaseInstance.Initialize(gameObject, this);
        enemyAttackBaseInstance.Initialize(gameObject, this);
        enemySpawnBase.Initialize(gameObject, this);
        enemyDeathBase.Initialize(gameObject, this);

        stateMachine.Initialize(spawnState);
    }

    private void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentEnemyState.PhysicsUpdate();
    }

#region Change states for animation events
    public void GoToIdle()
    {
        stateMachine.ChangeState(idleState);

    }

    public void GoToChase()
    {
        stateMachine.ChangeState(chaseState);

    }

    public void GoToAttack()
    {
        stateMachine.ChangeState(attackState);

    }

    public void GoToSpawn()
    {
        stateMachine.ChangeState(spawnState);

    }

    public void GoToDeath()
    {
        stateMachine.ChangeState(deathState);

    }
    #endregion


    #region Health Die Functions
    public void Damage(float damageAmount)
    {
        animator.SetBool("Hit", true);
        currentHealth -= damageAmount;
        if (currentHealth <= 0f) {
           stateMachine.ChangeState(deathState);

        }
        animator.SetBool("Hit", false);
    }

    public void Die()
    {
        
        Destroy(gameObject);
        
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;
        CheckLeftOrRightFacing(velocity);
        
    }

    public void CheckLeftOrRightFacing(Vector2 velocity)
    {
        if (isFacingRight && velocity.x < 0f)
        {
            isFacingRight = !isFacingRight;
            animator.SetBool("FacingLeft", true);
            Debug.Log(animator.GetBool("FacingLeft"));
        }
        else if (!isFacingRight && velocity.x > 0f) {
            isFacingRight = !isFacingRight;
            animator.SetBool("FacingLeft", false);
            Debug.Log(animator.GetBool("FacingLeft"));
        }
    }

    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType) { 
        stateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType { 
        EnemyDamaged,
        PlayFootStepSound
    
    }
    #endregion

    #region Trigger Checks
    public void setAggroStatus(bool isAggroed_)
    {
        isAggroed = isAggroed_;
    }

    public void setStrikingDistance(bool isStrikingDistance_)
    {
        isWithinStikingDistance = isStrikingDistance_;
    }
    #endregion
}
