using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

public class PlayerController : MonoBehaviour, IGamePlayActions
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
        _tr = GetComponent<TrailRenderer>();
        _abilityManager = GetComponent<AbilityManager>();
        _playerState = GetComponent<PlayerStateMachine>();

        //Set up input actions
        _playerInput.GamePlay.Movement.started += OnMovement;
        _playerInput.GamePlay.Movement.performed += OnMovement;
        _playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        _playerInput.GamePlay.Dash.started += OnDash;
        _playerInput.GamePlay.Dash.performed += OnDash;
        _playerInput.GamePlay.Dash.canceled += OnDash;

        _playerInput.GamePlay.Primary.started += OnPrimary;
        _playerInput.GamePlay.Primary.performed += OnPrimary;
        _playerInput.GamePlay.Primary.canceled += OnPrimary;

        _playerInput.GamePlay.Secondary.started += OnSecondary;
        _playerInput.GamePlay.Secondary.performed += OnSecondary;
        _playerInput.GamePlay.Secondary.canceled += OnSecondary;
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

        _playerInput.GamePlay.Dash.started += OnDash;
        _playerInput.GamePlay.Dash.performed += OnDash;
        _playerInput.GamePlay.Dash.canceled += OnDash;

        _playerInput.GamePlay.Primary.started += OnPrimary;
        _playerInput.GamePlay.Primary.performed += OnPrimary;
        _playerInput.GamePlay.Primary.canceled += OnPrimary;

        _playerInput.GamePlay.Secondary.started += OnSecondary;
        _playerInput.GamePlay.Secondary.performed += OnSecondary;
        _playerInput.GamePlay.Secondary.canceled += OnSecondary;

    }

    //Disables Input Actions
    private void OnDisable()
    {
        _playerInput.GamePlay.Disable();
        _playerInput.GamePlay.Movement.started -= OnMovement;
        _playerInput.GamePlay.Movement.performed -= OnMovement;
        _playerInput.GamePlay.Movement.canceled -= OnMovementCancel;

        _playerInput.GamePlay.Dash.started -= OnDash;
        _playerInput.GamePlay.Dash.performed -= OnDash;
        _playerInput.GamePlay.Dash.canceled -= OnDash;

        _playerInput.GamePlay.Primary.started -= OnPrimary;
        _playerInput.GamePlay.Primary.performed -= OnPrimary;
        _playerInput.GamePlay.Primary.canceled -= OnPrimary;

        _playerInput.GamePlay.Secondary.started -= OnSecondary;
        _playerInput.GamePlay.Secondary.performed -= OnSecondary;
        _playerInput.GamePlay.Secondary.canceled -= OnSecondary;
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
        //DisableMovement();
        _canDash = false;

        _tr.emitting = true;
        _rb.velocity = _faceDirection * _dashingPower;
        yield return new WaitForSeconds(_dashingTime);

        //-----------------------------------------------------------------------------------

        //Handles Dash End
        //EnableMovement();
        _tr.emitting = false;
        _playerState.State = Enum_State.IDLING;
        _rb.velocity = Vector2.zero;

        //Handles Dash Cooldown
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
    #endregion

    #region Primary
    public void OnPrimary(InputAction.CallbackContext context)
    {
        _abilityManager.OnPrimary(context);
    }
    #endregion

    #region Secondary
    public void OnSecondary(InputAction.CallbackContext context)
    {
        _abilityManager.OnSecondary(context);
    }
    #endregion
}
