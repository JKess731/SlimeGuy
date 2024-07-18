using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Movement Variables
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    //Dash Variables
    [Header("Dash Variables")]
    [SerializeField] private float dashingPower = 20f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;
    private TrailRenderer tr;
    private bool dashPressed;
    private bool canDash = true;

    //Input Variables
    private PlayerInput playerInput;
    public Vector2 moveVector { get; private set; }
    public Vector2 faceDirection { get; private set; }
    
    //Player State Enum
    [Header("Animation States")]
    private PlayerState state;
    
    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();

        //Set up input actions
        OnEnable();
        playerInput.GamePlay.Movement.started += OnMovement;
        playerInput.GamePlay.Movement.performed += OnMovement;
        playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        playerInput.GamePlay.Dash.started += OnDash;
        playerInput.GamePlay.Dash.performed += OnDash;
        playerInput.GamePlay.Dash.canceled += OnDash;
    }

    private void Start()
    {
        state = PlayerState.IDLE;
    }
    private void Update()
    {
        HandleMovement();
    }

    //Enables Input Actions
    private void OnEnable()
    {
        playerInput.GamePlay.Enable();
        playerInput.GamePlay.Movement.started += OnMovement;
        playerInput.GamePlay.Movement.performed += OnMovement;
        playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        playerInput.GamePlay.Dash.started += OnDash;
        playerInput.GamePlay.Dash.performed += OnDash;
        playerInput.GamePlay.Dash.canceled += OnDash;
    }

    //Disables Input Actions
    private void OnDisable()
    {
        playerInput.GamePlay.Disable();
        playerInput.GamePlay.Movement.started -= OnMovement;
        playerInput.GamePlay.Movement.performed -= OnMovement;
        playerInput.GamePlay.Movement.canceled -= OnMovementCancel;

        playerInput.GamePlay.Dash.started -= OnDash;
        playerInput.GamePlay.Dash.performed -= OnDash;
        playerInput.GamePlay.Dash.canceled -= OnDash;
    }

    public void DisableMovement()
    {
        playerInput.GamePlay.Movement.started -= OnMovement;
        playerInput.GamePlay.Movement.performed -= OnMovement;
        playerInput.GamePlay.Movement.canceled -= OnMovement;
    }

    //Enables Movement Input Actions
    public void EnableMovement()
    {
        playerInput.GamePlay.Movement.started += OnMovement;
        playerInput.GamePlay.Movement.performed += OnMovement;
        playerInput.GamePlay.Movement.canceled += OnMovementCancel;
    }

    //Handles Movement
    private void HandleMovement()
    {
        if(state == PlayerState.DAMAGED || state == PlayerState.DASHING)
        {
            return;
        }

        if (state == PlayerState.DASHING)
        {
            StartCoroutine(DashCoroutine());
        }

        if (state == PlayerState.IDLE || state == PlayerState.MOVING)
        {
            rb.velocity = moveVector * speed;
        }
    }

    //Handles Movement Input Actions
    private void OnMovement(InputAction.CallbackContext context)
    {
        state = PlayerState.MOVING;

        moveVector = context.ReadValue<Vector2>();
        faceDirection = moveVector.normalized;
    }

    private void OnMovementCancel(InputAction.CallbackContext context)
    {
        state = PlayerState.IDLE;

        moveVector = Vector2.zero;
    }

    //Handles Dash Input Actions
    private void OnDash(InputAction.CallbackContext context)
    {
        state = PlayerState.DASHING;

        dashPressed = context.ReadValueAsButton();
    }

    //Dash Coroutine set the rigidbody to the dashing power for a set amount of time
    private IEnumerator DashCoroutine()
    {
        //Handles Initial Dash
        AudioManager.instance.PlayOneShot(FmodEvents.instance.playerDash, transform.position);
        DisableMovement();
        state = PlayerState.DASHING;
        canDash = false;

        tr.emitting = true;
        rb.velocity = faceDirection * dashingPower;
        yield return new WaitForSeconds(dashingTime);

        //-----------------------------------------------------------------------------------

        //Handles Dash End
        EnableMovement();
        tr.emitting = false;
        state = PlayerState.IDLE;
        rb.velocity = Vector2.zero;

        //Handles Dash Cooldown
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

enum PlayerState
{
    IDLE,
    MOVING,
    DASHING,
    ATTACKING,
    DAMAGED
}
