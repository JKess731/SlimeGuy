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
    private PlayerStateMachine _playerState;
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
        _abilityManager = GetComponent<AbilityManager>();
        _playerState = GetComponent<PlayerStateMachine>();

        _speed = _playerState.playerStats.GetStat(StatsEnum.SPEED);

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
        _playerState.SetState(Enum_State.IDLING);
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
        if(_playerState.State == Enum_State.DAMAGED || _playerState.State == Enum_State.DASHING)
        {
            return;
        }

        if (_playerState.State == Enum_State.IDLING || _playerState.State == Enum_State.MOVING)
        {
            _rb.velocity = _moveVector * _speed;
        }
    }

    //Handles Movement Input Actions
    public void OnMovement(InputAction.CallbackContext context)
    {
        _playerState.State = Enum_State.MOVING;

        _moveVector = context.ReadValue<Vector2>();
        _faceDirection = _moveVector.normalized;
    }

    public void OnMovementCancel(InputAction.CallbackContext context)
    {
        _playerState.State = Enum_State.IDLING;

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

        _playerState.State = Enum_State.DASHING;
        StartCoroutine(DashCoroutine());

        _dashPressed = context.ReadValueAsButton();
    }

    //Dash Coroutine set the rigidbody to the dashing power for a set amount of time
    private IEnumerator DashCoroutine()
    {
        //Handles Initial Dash
        AudioManager.instance.PlayOneShot(FmodEvents.instance.playerDash, transform.position);
        //Debug.Log("Dash Started");
        _playerState.State = Enum_State.DASHING;
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
        _playerState.State = Enum_State.IDLING;
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

    #region Secondary
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
    #endregion

    #region Dash
    private void OnDashStarted(InputAction.CallbackContext context)
    {
        if (_canDash)
        {
            StartCoroutine(DashCoroutine());
        }
        _abilityManager.OnDashStarted(context);
    }
    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        _abilityManager.OnDashPerformed(context);
    }
    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        _abilityManager.OnDashCanceled(context);
    }

    public void OnPrimary(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSecondary(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
