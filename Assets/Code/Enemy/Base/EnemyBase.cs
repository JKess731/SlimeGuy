using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [Header("Enemy StatSO")]
    [SerializeField] private StatsSO _stats; 

    [Header("Enemy StateSO")]
    [Space()]
    //The scriptable objects that hold the base logic for the enemy
    #region Scriptable Objects Variables
    [SerializeField] private EnemySpawnSOBase enemySpawnBase;
    [SerializeField] private EnemyIdleSOBase enemyIdleBase;
    [SerializeField] private EnemyChaseSOBase enemyChaseBase;
    [SerializeField] private EnemyAttackSOBase enemyAttackBase;
    [SerializeField] private EnemyDamagedSOBase enemyDamagedBase;
    [SerializeField] private EnemyDeathSOBase enemyDeathBase;

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

    //The instances of the scriptable objects 
    public EnemySpawnSOBase enemySpawnBaseInstance { get; set; }
    public EnemyIdleSOBase enemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase enemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase enemyAttackBaseInstance { get; set; }
    public EnemyDamagedSOBase enemyDamagedBaseInstance { get; set; }
    public EnemyDeathSOBase enemyDeathBaseInstance { get; set; }
    #endregion

    #region Trigger Variables
    public bool isAggroed { get; set; }
    public bool isWithinStikingDistance { get; set; }
    #endregion
    public  Rigidbody2D RB { get; set; }
    private Vector2 faceDir { get; set; }

    private KnockBack knockBack;                        // Knockback script
    private SimpleFalsh damageFlash;                    // Flash script

    private EnemyAnimation enemyAnimation;              // Animator for the enemy
    private Enum_State _state;                          // The current state of the enemy

    //public GameObject slimeDrop;                        // The slime drop prefab for absorption
    public bool isDead { get; set; } = false;

    public Vector2 FaceDir { get => faceDir; set => faceDir = value; }
    public Enum_State State { get => _state; set => _state = value; }

    private void Awake()
    {
        //Get Knockback and Flash Scripts
        knockBack = GetComponent<KnockBack>();
        damageFlash = GetComponent<SimpleFalsh>();
        enemyAnimation = GetComponent<EnemyAnimation>();

        //Instantiate Scriptable Objects
        _stats = Instantiate(_stats);
        _stats.Initialize();


        //Instantiate State Machine
        stateMachine = new EnemyStateMachine();

        //Instantiate enemy base scriptable objects
        enemySpawnBaseInstance = Instantiate(enemySpawnBase);
        enemyIdleBaseInstance = Instantiate(enemyIdleBase);
        enemyChaseBaseInstance = Instantiate(enemyChaseBase);
        enemyAttackBaseInstance = Instantiate(enemyAttackBase);
        enemyDamagedBaseInstance = Instantiate(enemyDamagedBase);
        enemyDeathBaseInstance = Instantiate(enemyDeathBase);

        //Instantiate enemy states into State Machine
        spawnState = new EnemySpawningState(this, stateMachine);
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        damagedState = new EnemyDamagedState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();

        enemySpawnBaseInstance.Initialize(gameObject, this);
        enemyIdleBaseInstance.Initialize(gameObject, this);
        enemyChaseBaseInstance.Initialize(gameObject, this);
        enemyAttackBaseInstance.Initialize(gameObject, this);
        enemyDamagedBaseInstance.Initialize(gameObject, this);
        enemyDeathBaseInstance.Initialize(gameObject, this);

        stateMachine.Initialize(spawnState);
    }

    private void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
        enemyAnimation.PlayAnimation(faceDir,_state);
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

    public void GoToDamage()
    {
        stateMachine.ChangeState(damagedState);
    }
    #endregion

    #region Health Die Functions
    public void Damage(float damageAmount, Vector2 hitDirection, float hitforce, Vector2 constantForceDirection)
    {
        damageFlash.Flash();
        _stats.SubtractStat(StatsEnum.HEALTH, damageAmount);
        //Debug.Log(_stats.GetStat(StatsEnum.HEALTH));
        GoToDamage();

        knockBack.CallKnockback(hitDirection, hitforce, constantForceDirection);

        if (_stats.GetStat(StatsEnum.HEALTH) <=0) {
            Die();
        }
    }
    public void Damage(float damageAmount)
    {
        damageFlash.Flash();
        _stats.SubtractStat(StatsEnum.HEALTH, damageAmount);
        //Debug.Log(_stats.GetStat(StatsEnum.HEALTH));
        GoToDamage();

        if (_stats.GetStat(StatsEnum.HEALTH) <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _stats.SetStat(StatsEnum.SPEED, 0);
        //Instantiate(slimeDrop, transform.position, Quaternion.identity);
        isDead = true;  //Prevent multiple slimedrops
        stateMachine.ChangeState(deathState);
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        faceDir = velocity.normalized;
        RB.velocity = velocity * _stats.GetStat(StatsEnum.SPEED);
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
    #endregion

    #region Animation Triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType) { 
        stateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType { 
        DwarfDamaged,
        DwarfAttack,
        DwarfDeath,
        PlayDwarfFootStepSound,
        GolemAttack,
        GolemDeath,
        GolemDamaged,
        GolemFootStepSound
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
