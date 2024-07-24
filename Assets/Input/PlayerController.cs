using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

public class PlayerController : MonoBehaviour, IGamePlayActions
{
    //Movement Variables
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform attackPos;

    //Dash Variables
    [Header("Dash Variables")]
    [SerializeField] private float dashingPower = 20f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;
    private bool dashPressed;
    private bool canDash = true;
    private TrailRenderer tr;

    //Ability Variables
    [Header("Ability Variables")]
    [SerializeField] private AbilityBase primary;
    [SerializeField] private AbilityBase secondary;
    [SerializeField] private AbilityBase dash;

    //Input Variables
    public PlayerInput playerInput { get; private set; }
    public Vector2 moveVector { get; private set; }
    public Vector2 faceDirection { get; private set; }

    //Player State Enum
    public PlayerState state { get; private set; }

    private void Awake()
    {
        if(playerInput == null)
        {
            playerInput = new PlayerInput();
        }
        
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();

        //Set up input actions
        playerInput.GamePlay.Movement.started += OnMovement;
        playerInput.GamePlay.Movement.performed += OnMovement;
        playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        playerInput.GamePlay.Dash.started += OnDash;
        playerInput.GamePlay.Dash.performed += OnDash;
        playerInput.GamePlay.Dash.canceled += OnDash;

        playerInput.GamePlay.Primary.started += OnPrimary;
        playerInput.GamePlay.Primary.performed += OnPrimary;
        playerInput.GamePlay.Primary.canceled += OnPrimary;

        playerInput.GamePlay.Secondary.started += OnSecondary;
        playerInput.GamePlay.Secondary.performed += OnSecondary;
        playerInput.GamePlay.Secondary.canceled += OnSecondary;

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

    #region Movement
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

        if (state == PlayerState.IDLE || state == PlayerState.MOVING)
        {
            rb.velocity = moveVector * speed;
        }
    }

    //Handles Movement Input Actions
    public void OnMovement(InputAction.CallbackContext context)
    {
        state = PlayerState.MOVING;

        moveVector = context.ReadValue<Vector2>();
        faceDirection = moveVector.normalized;
    }

    public void OnMovementCancel(InputAction.CallbackContext context)
    {
        state = PlayerState.IDLE;

        moveVector = Vector2.zero;
    }
    #endregion

    #region Dash
    //Handles Dash Input Actions
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!canDash)
        {
            return;
        }

        state = PlayerState.DASHING;
        StartCoroutine(DashCoroutine());

        dashPressed = context.ReadValueAsButton();
    }

    //Dash Coroutine set the rigidbody to the dashing power for a set amount of time
    private IEnumerator DashCoroutine()
    {
        //Handles Initial Dash
        AudioManager.instance.PlayOneShot(FmodEvents.instance.playerDash, transform.position);
        //DisableMovement();
        canDash = false;

        tr.emitting = true;
        rb.velocity = faceDirection * dashingPower;
        yield return new WaitForSeconds(dashingTime);

        //-----------------------------------------------------------------------------------

        //Handles Dash End
        //EnableMovement();
        tr.emitting = false;
        state = PlayerState.IDLE;
        rb.velocity = Vector2.zero;

        //Handles Dash Cooldown
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    #endregion

    #region Primary
    public void OnPrimary(InputAction.CallbackContext context)
    {
        primary.ActivateAbility(context, attackPos.rotation, attackPos.position);
    }
    #endregion

    #region Secondary
    public void OnSecondary(InputAction.CallbackContext context)
    {
        secondary.ActivateAbility(context, attackPos.rotation, attackPos.position);
    }
    #endregion
}
