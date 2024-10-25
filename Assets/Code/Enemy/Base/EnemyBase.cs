using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    //-------------------------------------------------------
    //                  Serialized Fields
    //-------------------------------------------------------
    [Header("Enemy Stats SO")]
    [SerializeField] private StatsSO _stats;
    [SerializeField] private float _knockbackRes;
    [SerializeField] private bool _isStunnable = true;

    [Header("Enemy Ring")]
    [SerializeField] private Transform ring;

    [Header("Enemy State Instance SO")]
    //The scriptable objects that hold the base logic for the enemy
    #region Scriptable Objects Variables
    [SerializeField] private EnemySpawnSOBase _enemySpawnBase;
    [SerializeField] private EnemyIdleSOBase _enemyIdleBase;
    [SerializeField] private EnemyMoveSOBase _enemyChaseBase;
    [SerializeField] private EnemyAttackSOBase _enemyAttackBase;
    [SerializeField] private EnemyDamagedSOBase _enemyDamagedBase;
    [SerializeField] private EnemyDeathSOBase _enemyDeathBase;
    //The instances of the scriptable objects 
    #endregion

    [Header("Enemy Sfx")]
    #region Sfx References
    [SerializeField] public List<EventReference> attackSoundEffects;
    [SerializeField] public List<EventReference> moveSoundEffects;
    [SerializeField] public List<EventReference> damagedSoundEffects;
    [SerializeField] public List<EventReference> deathSoundEffects;
    #endregion

    private Vector2 _faceDir;                            // The direction the enemy is facing
    private bool _isDead = false;                        // Is the enemy dead
    private KnockBack _knockBack;                        // Knockback script
    private SimpleFalsh _damageFlash;                    // Flash script

    private Rigidbody2D _rigidbody2D;                    // Rigidbody of the enemy
    private AnimationControl _enemyAnimation;            // Animator for the enemy
    private Enum_AnimationState _state;                           // The current state of the enemy

    public UnityEvent OnDeath;

    //-------------------------------------------------------
    //                  Non-Serialized Fields
    //-------------------------------------------------------
    private Coroutine _attackEnumerator;                          // Enumerator for the attack
    private Coroutine _stunEnumerator;
    public Coroutine AttackEnumerator { get => _attackEnumerator; set => _attackEnumerator = value; }   // Enumerator for the attack
    public Coroutine StunEnumerator { get => _stunEnumerator; set => _stunEnumerator = value; }         // Enumerator for the stun

    //-------------------------------------------------------
    //                         Properties
    //-------------------------------------------------------
    #region Regular Properties
    public StatsSO Stats { get => _stats; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public bool IsStunnable { get => _isStunnable; set => _isStunnable = value; }
    public KnockBack KnockBack { get => _knockBack;}
    public Vector2 FaceDir { get => _faceDir; set => _faceDir = value; }
    public Transform Ring { get => ring;}
    public Enum_AnimationState State { get => _state; set => _state = value; }
    public Rigidbody2D RigidBody2d { get => _rigidbody2D; set => _rigidbody2D = value; }
    public AnimationControl EnemyAnimation { get => _enemyAnimation;}
    #endregion
    //----------------- Trigger Variables -------------------    //The variables that hold the status of the enemy
    #region Trigger Variables
    public bool _isAggroed { get; set; }
    public bool _isWithinStikingDistance { get; set; }
    public bool _isWithinShootingDistance { get; set; }
    public bool _isWithinTeleportingDistance { get; set; }
    public bool _isWithinRunAwayDistance { get; set; }  
    #endregion
    //---------------State Machine Variables-----------------    //The types of states the enemy can be in
    #region State Machine Variables
    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyMoveState moveState { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemyDamagedState damagedState { get; set; }
    public EnemySpawningState spawnState { get; set; }
    public EnemyDeathState deathState { get; set; }
    #endregion
    //---------------Scriptable Object Instances-------------    //The instances of the scriptable objects
    #region SO Instances Variables
    public EnemySpawnSOBase enemySpawnBaseInstance { get; set; }
    public EnemyIdleSOBase enemyIdleBaseInstance { get; set; }
    public EnemyMoveSOBase enemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase enemyAttackBaseInstance { get; set; }
    public EnemyDamagedSOBase enemyDamagedBaseInstance { get; set; }
    public EnemyDeathSOBase enemyDeathBaseInstance { get; set; }
    #endregion

    private void Awake()
    {
        //Get attack ring transform
        ring = transform.Find("AttackRing").transform;

        //Get Knockback and Flash Scripts
        _knockBack = GetComponent<KnockBack>();
        _damageFlash = GetComponent<SimpleFalsh>();
        _enemyAnimation = GetComponent<AnimationControl>();

        //Instantiate Scriptable Objects
        _stats = Instantiate(_stats);
        _stats.Initialize();

        //Instantiate State Machine
        stateMachine = new EnemyStateMachine();

        //Instantiate enemy base scriptable objects
        enemySpawnBaseInstance = Instantiate(_enemySpawnBase);
        enemyIdleBaseInstance = Instantiate(_enemyIdleBase);
        enemyChaseBaseInstance = Instantiate(_enemyChaseBase);
        enemyAttackBaseInstance = Instantiate(_enemyAttackBase);
        enemyDamagedBaseInstance = Instantiate(_enemyDamagedBase);
        enemyDeathBaseInstance = Instantiate(_enemyDeathBase);

        //Instantiate enemy states into State Machine
        spawnState = new EnemySpawningState(this, stateMachine);
        idleState = new EnemyIdleState(this, stateMachine);
        moveState = new EnemyMoveState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        damagedState = new EnemyDamagedState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);
    }

    private void Start()
    {
        RigidBody2d = GetComponent<Rigidbody2D>();

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
        var angle = Mathf.Atan2(_faceDir.y, _faceDir.x) * Mathf.Rad2Deg;
        ring.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        _enemyAnimation.PlayAnimation(_faceDir,_state); //This line handles animation <--- Check this here for animation troubles may need to be changed
        stateMachine.currentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentEnemyState.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        //OnDeath?.Invoke();
    }

    #region Change states for animation events
    public void GoToIdle()
    {
        stateMachine.ChangeState(idleState);
    }

    public void GoToChase()
    {
        stateMachine.ChangeState(moveState);
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
        if (_stats.GetStat(Enum_Stats.HEALTH) <=0 && !_isDead) {
            _damageFlash.Flash();
            Die();
        }

        if (_stats.GetStat(Enum_Stats.HEALTH) >= 0 && !_isDead)
        {
            _damageFlash.Flash();
            _stats.SubtractStat(Enum_Stats.HEALTH, damageAmount);
            _knockBack.CallKnockback(hitDirection, hitforce, constantForceDirection);
            AudioManager.PlayOneShot(damagedSoundEffects[0], transform.position);
        }

        if (_isStunnable && !_isDead)
        {
            GoToDamage();
        }
    }
    public void Damage(float damageAmount)
    {
        if (_stats.GetStat(Enum_Stats.HEALTH) <= 0 && !_isDead)
        {
            _damageFlash.Flash();
            Die();
        }

        if (_stats.GetStat(Enum_Stats.HEALTH) >= 0 && !_isDead)
        {
            _damageFlash.Flash();
            _stats.SubtractStat(Enum_Stats.HEALTH, damageAmount);
            AudioManager.PlayOneShot(damagedSoundEffects[0], transform.position);
        }

        //Debug.Log(_stats.GetStat(StatsEnum.HEALTH));
        if (_isStunnable && !_isDead)
        {
            GoToDamage();
            Debug.Log("Damaged");
        }
    }

    public void Die()
    {
        _stats.SetStat(Enum_Stats.SPEED, 0);
        stateMachine.ChangeState(deathState);
        _isDead = true;  
        Debug.Log(stateMachine.currentEnemyState);
        Debug.Log(_state);
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        RigidBody2d.velocity = velocity;
    }

    /// <summary>
    /// Modifiys the movement speed of this enemy by += operation
    /// </summary>
    /// <param name="amount"></param>
    public void ModifyMoveSpeed(float amount, float timeUntilReset)
    {
        float originalSpeed = _stats.GetStat(Enum_Stats.SPEED);
        _stats.AddStat(Enum_Stats.SPEED,amount);

        StartCoroutine(ResetSpeed(timeUntilReset, originalSpeed));
    }

    public IEnumerator ResetSpeed(float time, float originalSpeed)
    {
        yield return new WaitForSeconds(time);
        _stats.SetStat(Enum_Stats.SPEED, originalSpeed);
    }
    #endregion

    #region Animation Triggers
    private void AnimationTriggerEvent(Enum_AnimationTriggerType triggerType) { 
        stateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }
    #endregion

    #region Trigger Checks
    public void setAggroStatus(bool isAggroed)
    {
        _isAggroed = isAggroed;
    }

    public void setStrikingDistance(bool isStrikingDistance)
    {
        _isWithinStikingDistance = isStrikingDistance;
    }

    public void setShootingDistance(bool isShootingDistance) 
    {
        _isWithinShootingDistance = isShootingDistance;
    }

    public void setTeleportingDistance(bool isTeleportingDistance)
    {
        _isWithinTeleportingDistance = isTeleportingDistance;
    }

    public void setRunAwayDistance(bool isRunAwayDistance)
    {
        _isWithinRunAwayDistance = isRunAwayDistance;
    }
    #endregion
}
