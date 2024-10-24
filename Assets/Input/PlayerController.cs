using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

public class PlayerController : MonoBehaviour
{
    //Movement Variables
    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 10f;

    //Dash Variables
    [Header("Dash Variables")]
    [SerializeField] private float _dashingPower = 20f;
    [SerializeField] private float _dashingTime = 0.5f;
    [SerializeField] private float _dashingCooldown = 1f;
    private bool _dashPressed;
    private bool _canDash = true;
    private TrailRenderer _tr;

    //Ability Variables
    [SerializeField] private AbilityManager _abilityManager;

    //PlayerStateMachine Variables
    private PlayerStateMachine _playerStateMachine;
    private Collider2D _playerCollider;

    //Input Variables
    private PlayerInput _playerInput;
    private Vector2 _moveVector;
    private Vector2 _faceDirection;

    public PlayerInput PlayerInput { get => _playerInput;}
    public Vector2 FaceDirection { get => _faceDirection; set => _faceDirection = value; }
    public Vector2 MoveVector { get => _moveVector; set => _moveVector = value; }

    private void Awake()
    {
        if(_playerInput == null)
        {
            _playerInput = new PlayerInput();
        }

        _rb = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<Collider2D>();
        _tr = GetComponent<TrailRenderer>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();

        _abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();

        _speed = _playerStateMachine.playerStats.GetStat(Enum_Stats.SPEED);

        //Set up input actions
        _playerInput.GamePlay.Movement.started += OnMovement;
        _playerInput.GamePlay.Movement.performed += OnMovement;
        _playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        _playerInput.GamePlay.Dash.started += OnDashStarted;
        _playerInput.GamePlay.Dash.performed += OnDashPerformed;
        _playerInput.GamePlay.Dash.canceled += OnDashCanceled;

        _playerInput.GamePlay.Primary.started += OnPrimaryStarted;
        _playerInput.GamePlay.Primary.performed += OnPrimaryPerformed;
        _playerInput.GamePlay.Primary.canceled += OnPrimaryCanceled;

        _playerInput.GamePlay.Secondary.started += OnSecondaryStarted;
        _playerInput.GamePlay.Secondary.performed += OnSecondaryPerformed;
        _playerInput.GamePlay.Secondary.canceled += OnSecondaryCanceled;
    }

    private void Start()
    {
        _playerStateMachine.SetState(Enum_AnimationState.IDLING);
    }
    private void Update()
    {
        HandleMovement();
    }

    //Enables Input Actions
    private void OnEnable()
    {
        _playerInput.GamePlay.Enable();
        _playerInput.GamePlay.Movement.started += OnMovement;
        _playerInput.GamePlay.Movement.performed += OnMovement;
        _playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        _playerInput.GamePlay.Dash.started += OnDashStarted;
        _playerInput.GamePlay.Dash.performed += OnDashPerformed;
        _playerInput.GamePlay.Dash.canceled += OnDashCanceled;

        _playerInput.GamePlay.Primary.started += OnPrimaryStarted;
        _playerInput.GamePlay.Primary.performed += OnPrimaryPerformed;
        _playerInput.GamePlay.Primary.canceled += OnPrimaryCanceled;

        _playerInput.GamePlay.Secondary.started += OnSecondaryStarted;
        _playerInput.GamePlay.Secondary.performed += OnSecondaryPerformed;
        _playerInput.GamePlay.Secondary.canceled += OnSecondaryCanceled;

        _playerInput.GamePlay.Secondary2.started += OnSecondary2Started;
        _playerInput.GamePlay.Secondary2.performed += OnSecondary2Performed;
        _playerInput.GamePlay.Secondary2.canceled += OnSecondary2Canceled;
    }

    //Disables Input Actions
    private void OnDisable()
    {
        _playerInput.GamePlay.Disable();
        _playerInput.GamePlay.Movement.started -= OnMovement;
        _playerInput.GamePlay.Movement.performed -= OnMovement;
        _playerInput.GamePlay.Movement.canceled -= OnMovementCancel;

        _playerInput.GamePlay.Dash.started -= OnDashStarted;
        _playerInput.GamePlay.Dash.performed -= OnDashPerformed;
        _playerInput.GamePlay.Dash.canceled -= OnDashCanceled;

        _playerInput.GamePlay.Primary.started -= OnPrimaryStarted;
        _playerInput.GamePlay.Primary.performed -= OnPrimaryPerformed;
        _playerInput.GamePlay.Primary.canceled -= OnPrimaryCanceled;

        _playerInput.GamePlay.Secondary.started -= OnSecondaryStarted;
        _playerInput.GamePlay.Secondary.performed -= OnSecondaryPerformed;
        _playerInput.GamePlay.Secondary.canceled -= OnSecondaryCanceled;

        _playerInput.GamePlay.Secondary2.started -= OnSecondary2Started;
        _playerInput.GamePlay.Secondary2.performed -= OnSecondary2Performed;
        _playerInput.GamePlay.Secondary2.canceled -= OnSecondary2Canceled;
    }

    public void DisableGameplay()
    {
        _playerInput.GamePlay.Disable();
        _playerInput.GamePlay.Movement.started -= OnMovement;
        _playerInput.GamePlay.Movement.performed -= OnMovement;
        _playerInput.GamePlay.Movement.canceled -= OnMovementCancel;

        _playerInput.GamePlay.Dash.started -= OnDashStarted;
        _playerInput.GamePlay.Dash.performed -= OnDashPerformed;
        _playerInput.GamePlay.Dash.canceled -= OnDashCanceled;

        _playerInput.GamePlay.Primary.started -= OnPrimaryStarted;
        _playerInput.GamePlay.Primary.performed -= OnPrimaryPerformed;
        _playerInput.GamePlay.Primary.canceled -= OnPrimaryCanceled;

        _playerInput.GamePlay.Secondary.started -= OnSecondaryStarted;
        _playerInput.GamePlay.Secondary.performed -= OnSecondaryPerformed;
        _playerInput.GamePlay.Secondary.canceled -= OnSecondaryCanceled;

        _playerInput.GamePlay.Secondary2.started -= OnSecondary2Started;
        _playerInput.GamePlay.Secondary2.performed -= OnSecondary2Performed;
        _playerInput.GamePlay.Secondary2.canceled -= OnSecondary2Canceled;
    }

    public void EnableGameplay()
    {
        _playerInput.GamePlay.Enable();
        _playerInput.GamePlay.Movement.started += OnMovement;
        _playerInput.GamePlay.Movement.performed += OnMovement;
        _playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        _playerInput.GamePlay.Dash.started += OnDashStarted;
        _playerInput.GamePlay.Dash.performed += OnDashPerformed;
        _playerInput.GamePlay.Dash.canceled += OnDashCanceled;

        _playerInput.GamePlay.Primary.started += OnPrimaryStarted;
        _playerInput.GamePlay.Primary.performed += OnPrimaryPerformed;
        _playerInput.GamePlay.Primary.canceled += OnPrimaryCanceled;

        _playerInput.GamePlay.Secondary.started += OnSecondaryStarted;
        _playerInput.GamePlay.Secondary.performed += OnSecondaryPerformed;
        _playerInput.GamePlay.Secondary.canceled += OnSecondaryCanceled;

        _playerInput.GamePlay.Secondary2.started += OnSecondary2Started;
        _playerInput.GamePlay.Secondary2.performed += OnSecondary2Performed;
        _playerInput.GamePlay.Secondary2.canceled += OnSecondary2Canceled;
    }

    #region Movement
    public void DisableMovement()
    {
        _playerInput.GamePlay.Movement.started -= OnMovement;
        _playerInput.GamePlay.Movement.performed -= OnMovement;
        _playerInput.GamePlay.Movement.canceled -= OnMovement;
    }

    //Enables Movement Input Actions
    public void EnableMovement()
    {
        _playerInput.GamePlay.Movement.started += OnMovement;
        _playerInput.GamePlay.Movement.performed += OnMovement;
        _playerInput.GamePlay.Movement.canceled += OnMovementCancel;
    }

    //Handles Movement
    private void HandleMovement()
    {
        if (_playerStateMachine.State == Enum_AnimationState.DEAD)
        {
            return;
        }

        if(_playerStateMachine.State == Enum_AnimationState.DAMAGED)
        {
            return;
        }

        if (_playerStateMachine.State == Enum_AnimationState.IDLING || _playerStateMachine.State == Enum_AnimationState.MOVING)
        {
            _rb.velocity = _moveVector * _speed;
        }
    }

    //Handles Movement Input Actions
    public void OnMovement(InputAction.CallbackContext context)
    {
        _playerStateMachine.State = Enum_AnimationState.MOVING;

        _moveVector = context.ReadValue<Vector2>();
        _faceDirection = _moveVector.normalized;
    }

    public void OnMovementCancel(InputAction.CallbackContext context)
    {
        _playerStateMachine.State = Enum_AnimationState.IDLING;

        _moveVector = Vector2.zero;
    }
    #endregion

    #region Dash
    //Handles Dash Input Actions
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!_canDash)
        {
            return;
        }

        _playerStateMachine.State = Enum_AnimationState.DASHING;
        StartCoroutine(DashCoroutine());

        _dashPressed = context.ReadValueAsButton();
    }

    //Dash Coroutine set the rigidbody to the dashing power for a set amount of time
    private IEnumerator DashCoroutine()
    {
        //Handles Initial Dash
        AudioManager.PlayOneShot(FmodEvents.instance.playerDash, transform.position);
        //Debug.Log("Dash Started");
        _playerStateMachine.State = Enum_AnimationState.DASHING;
        _playerCollider.excludeLayers = LayerMask.GetMask("enemyAttacksLayer", "enemyLayer");
        //DisableMovement();
        _canDash = false;

        _tr.emitting = true;
        _rb.velocity = _faceDirection * _dashingPower;
        yield return new WaitForSeconds(_dashingTime);
        //Debug.Log("Dash Ended");
        //-----------------------------------------------------------------------------------

        //Handles Dash End
        //EnableMovement();
        _tr.emitting = false;
        _playerStateMachine.State = Enum_AnimationState.IDLING;
        _playerCollider.excludeLayers = 0;
        _rb.velocity = Vector2.zero;

        //Handles Dash Cooldown
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
    #endregion

    #region Primary

    private void OnPrimaryStarted(InputAction.CallbackContext context)
    {
        _abilityManager.OnPrimaryStarted(context);
    }
    private void OnPrimaryPerformed(InputAction.CallbackContext context)
    {
        _abilityManager.OnPrimaryPerformed(context);
    }
    private void OnPrimaryCanceled(InputAction.CallbackContext context)
    {
        _abilityManager.OnPrimaryCanceled(context);
    }
    #endregion

    #region All Secondary
    //Secondary 1 input: LeftClick ||
    private void OnSecondaryStarted(InputAction.CallbackContext context)
    {
        _abilityManager.OnSecondaryStarted(context);
    }
    private void OnSecondaryPerformed(InputAction.CallbackContext context)
    {
        _abilityManager.OnSecondaryPerformed(context);
    }
    private void OnSecondaryCanceled(InputAction.CallbackContext context)
    {
        _abilityManager.OnSecondaryCanceled(context);
    }

    //Secondary 2 input: Q ||
    private void OnSecondary2Started(InputAction.CallbackContext context)
    {
        _abilityManager.OnSecondary2Started(context);
    }
    private void OnSecondary2Performed(InputAction.CallbackContext context)
    {
        _abilityManager.OnSecondary2Performed(context);
    }
    private void OnSecondary2Canceled(InputAction.CallbackContext context)
    {
        _abilityManager.OnSecondary2Canceled(context);
    }
    #endregion

    #region Dash
    private void OnDashStarted(InputAction.CallbackContext context)
    {
        if (_canDash)
        {
            StartCoroutine(DashCoroutine());
            _abilityManager.OnDashStarted(context);
        }
    }
    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        _abilityManager.OnDashPerformed(context);
    }
    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        _abilityManager.OnDashCanceled(context);
    }
    #endregion
}
