using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    public Stats _stats;

    private Stats _statInstance;   

    public float currentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool isFacingRight { get; set; } = true;

    public Vector2 faceDir { get; set; }

    //The types of states the enemy can be in
    #region State Machine Variables
    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemyDamagedState damagedState { get; set; }
    public EnemySpawningState spawnState { get; set; }
    public EnemyDeathState deathState { get; set; }
    #endregion

    //The scriptable objects that hold the base logic for the enemy
    #region Scriptable Objects Variables
    [SerializeField] private EnemyIdleSOBase enemyIdleBase;
    [SerializeField] private EnemyChaseSOBase enemyChaseBase;
    [SerializeField] private EnemyAttackSOBase enemyAttackBase;
    //[SerializeField] private EnemyDamagedSOBase enemyDamagedBase;
    [SerializeField] private EnemySpawnSOBase enemySpawnBase;
    [SerializeField] private EnemyDeathSOBase enemyDeathBase;

    //The instances of the scriptable objects 
    public EnemyIdleSOBase enemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase enemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase enemyAttackBaseInstance { get; set; }
    //public EnemyDamagedSOBase enemyDamagedBaseInstance { get; set; }
    public EnemySpawnSOBase enemySpawnBaseInstance { get; set; }
    public EnemyDeathSOBase enemyDeathBaseInstance { get; set; }

    #endregion

    #region Idle Variables
    #endregion

    #region Trigger Variables
    public bool isAggroed { get; set; }
    public bool isWithinStikingDistance { get; set; }
    #endregion

    public KnockBack knockBack { get; private set; }    // Knockback script
    private SimpleFalsh damageFlash;                  // Flash script

    public Animator animator;
    public GameObject slimeDrop;                      // The slime drop prefab for absorption
    public bool isDead { get; set; } = false;
    public float maxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        //Get Knockback and Flash Scripts
        knockBack = GetComponent<KnockBack>();
        damageFlash = GetComponent<SimpleFalsh>();

        //Instantiate Scriptable Objects
        _statInstance = Instantiate(_stats);

        enemyIdleBaseInstance = Instantiate(enemyIdleBase);
        enemyChaseBaseInstance = Instantiate(enemyChaseBase);
        enemyAttackBaseInstance = Instantiate(enemyAttackBase);
        //enemyDamagedBaseInstance = Instantiate(enemyDamagedBase);
        enemySpawnBaseInstance = Instantiate(enemySpawnBase);
        enemyDeathBaseInstance = Instantiate(enemyDeathBase);

        //Instantiate State Machine
        stateMachine = new EnemyStateMachine();

        //Instantiate enemy states into State Machine
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        damagedState = new EnemyDamagedState(this, stateMachine);
        spawnState = new EnemySpawningState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();

        enemyIdleBaseInstance.Initialize(gameObject, this);
        enemyChaseBaseInstance.Initialize(gameObject, this);
        enemyAttackBaseInstance.Initialize(gameObject, this);
        //enemyDamagedBaseInstance.Initialize(gameObject, this);
        enemySpawnBase.Initialize(gameObject, this);
        enemyDeathBase.Initialize(gameObject, this);

        stateMachine.Initialize(spawnState);
    }

    private void Update()
    {
        if (knockBack.isBeingKnockedBack)
        {
            return;
        }
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
    public void Damage(float damageAmount, Vector2 hitDirection, float hitforce, Vector2 constantForceDirection)
    {
        damageFlash.Flash();
        animator.SetBool("Hit", true);
        _statInstance.SubtractStat(StatsEnum.HEALTH, damageAmount);

        knockBack.CallKnockback(hitDirection, hitforce, constantForceDirection);

        if (_statInstance.GetStat(StatsEnum.HEALTH) <=0 & !isDead) {
            Die();
        }
        animator.SetBool("Hit", false);
    }
    public void Damage(float damageAmount)
    {
        damageFlash.Flash();
        animator.SetBool("Hit", true);
        _statInstance.SubtractStat(StatsEnum.HEALTH, damageAmount);

        if (_statInstance.GetStat(StatsEnum.HEALTH) <= 0 & !isDead)
        {
            Die();
        }
        animator.SetBool("Hit", false);
    }

    public void Die()
    {
        Instantiate(slimeDrop, transform.position, Quaternion.identity);
        isDead = true;  //Prevent multiple slimedrops

        Destroy(gameObject);
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        faceDir = velocity.normalized;
        RB.velocity = velocity * _stats.GetStat(StatsEnum.SPEED);
        CheckLeftOrRightFacing(velocity);
    }

    public void CheckLeftOrRightFacing(Vector2 velocity)
    {
        if (isFacingRight && velocity.x < 0f)
        {
            isFacingRight = !isFacingRight;
            animator.SetBool("FacingLeft", true);
            //Debug.Log(animator.GetBool("FacingLeft"));
        }
        else if (!isFacingRight && velocity.x > 0f) {
            isFacingRight = !isFacingRight;
            animator.SetBool("FacingLeft", false);
            //Debug.Log(animator.GetBool("FacingLeft"));
        }
    }

    /// <summary>
    /// Modifiys the movement speed of this enemy by += operation
    /// </summary>
    /// <param name="amount"></param>
    public void ModifyMoveSpeed(float amount, float timeUntilReset)
    {
        float originalSpeed = _stats.GetStat(StatsEnum.SPEED);
        _stats.AddStat(StatsEnum.SPEED,amount);

        StartCoroutine(ResetSpeed(timeUntilReset, originalSpeed));
    }

    public IEnumerator ResetSpeed(float time, float originalSpeed)
    {
        yield return new WaitForSeconds(time);
        _stats.SetStat(StatsEnum.SPEED, originalSpeed);
    }

    public float GetSpeed()
    {
        return _stats.GetStat(StatsEnum.SPEED);
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
