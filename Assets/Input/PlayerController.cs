using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

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
    private bool dashPressed;
    private bool canDash = true;
    private TrailRenderer tr;

    //Ability Variables
    [SerializeField] private AbilityManager abilityManager;

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
        abilityManager = GetComponent<AbilityManager>();

        //Set up input actions
        playerInput.GamePlay.Movement.started += OnMovement;
        playerInput.GamePlay.Movement.performed += OnMovement;
        playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        playerInput.GamePlay.Dash.started += OnDashStarted;
        playerInput.GamePlay.Dash.performed += OnDashPerformed;
        playerInput.GamePlay.Dash.canceled += OnDashCanceled;

        playerInput.GamePlay.Primary.started += OnPrimaryStarted;
        playerInput.GamePlay.Primary.performed += OnPrimaryPerformed;
        playerInput.GamePlay.Primary.canceled += OnPrimaryCanceled;

        playerInput.GamePlay.Secondary.started += OnSecondaryStarted;
        playerInput.GamePlay.Secondary.performed += OnSecondaryPerformed;
        playerInput.GamePlay.Secondary.canceled += OnSecondaryCanceled;
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

        playerInput.GamePlay.Dash.started += OnDashStarted;
        playerInput.GamePlay.Dash.performed += OnDashPerformed;
        playerInput.GamePlay.Dash.canceled += OnDashCanceled;

        playerInput.GamePlay.Primary.started += OnPrimaryStarted;
        playerInput.GamePlay.Primary.performed += OnPrimaryPerformed;
        playerInput.GamePlay.Primary.canceled += OnPrimaryCanceled;

        playerInput.GamePlay.Secondary.started += OnSecondaryStarted;
        playerInput.GamePlay.Secondary.performed += OnSecondaryPerformed;
        playerInput.GamePlay.Secondary.canceled += OnSecondaryCanceled;
    }

    //Disables Input Actions
    private void OnDisable()
    {
        playerInput.GamePlay.Disable();
        playerInput.GamePlay.Movement.started -= OnMovement;
        playerInput.GamePlay.Movement.performed -= OnMovement;
        playerInput.GamePlay.Movement.canceled -= OnMovementCancel;

        playerInput.GamePlay.Dash.started -= OnDashStarted;
        playerInput.GamePlay.Dash.performed -= OnDashPerformed;
        playerInput.GamePlay.Dash.canceled -= OnDashCanceled;

        playerInput.GamePlay.Primary.started -= OnPrimaryStarted;
        playerInput.GamePlay.Primary.performed -= OnPrimaryPerformed;
        playerInput.GamePlay.Primary.canceled -= OnPrimaryCanceled;

        playerInput.GamePlay.Secondary.started -= OnSecondaryStarted;
        playerInput.GamePlay.Secondary.performed -= OnSecondaryPerformed;
        playerInput.GamePlay.Secondary.canceled -= OnSecondaryCanceled;
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

    private void OnPrimaryStarted(InputAction.CallbackContext context)
    {
        abilityManager.OnPrimaryStarted(context);
    }
    private void OnPrimaryPerformed(InputAction.CallbackContext context)
    {
        abilityManager.OnPrimaryPerformed(context);
    }
    private void OnPrimaryCanceled(InputAction.CallbackContext context)
    {
        abilityManager.OnPrimaryCanceled(context);
    }
    #endregion

    #region Secondary
    private void OnSecondaryStarted(InputAction.CallbackContext context)
    {
        abilityManager.OnSecondaryStarted(context);
    }
    private void OnSecondaryPerformed(InputAction.CallbackContext context)
    {
        abilityManager.OnSecondaryPerformed(context);
    }
    private void OnSecondaryCanceled(InputAction.CallbackContext context)
    {
        abilityManager.OnSecondaryCanceled(context);
    }
    #endregion

    #region Dash
    private void OnDashStarted(InputAction.CallbackContext context)
    {
        abilityManager.OnDashStarted(context);
    }
    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        abilityManager.OnDashPerformed(context);
    }
    private void OnDashCanceled(InputAction.CallbackContext context)
    {
        abilityManager.OnDashCanceled(context);
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
