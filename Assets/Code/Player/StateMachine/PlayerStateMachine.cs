using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //References
    PlayerHealth playerHealth;
    PlayerInput playerInput;
    AnimationControl animationControl;

    Rigidbody2D rb;
    TrailRenderer tr;

    //Movement Variables
    private Vector2 moveVector = Vector2.zero;
    public Vector2 faceDirection;

    //Dash Variables
    private bool canDash = true;
    private bool dashPressed;

    //Knockback Variables
    private KnockBack knockBack;

    //Dash Variables
    [Header("Dash Variables")]
    [SerializeField] private float dashingPower = 20f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;

    //Animation States
    [Header("Animation States")]
    [SerializeField] private bool isIdle = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isDamaged = false;

    //Player Speed
    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        //Not great
        playerHealth = GetComponent<PlayerHealth>();

        //Set up initial references
        animationControl = GetComponent<AnimationControl>();
        playerInput = new PlayerInput();

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        knockBack = GetComponent<KnockBack>();

        //Set up input actions
        playerInput.GamePlay.Movement.started += OnMovement;
        playerInput.GamePlay.Movement.performed += OnMovement;
        playerInput.GamePlay.Movement.canceled += OnMovementCancel;

        playerInput.GamePlay.Dash.started += OnDash;
        playerInput.GamePlay.Dash.performed += OnDash;
        playerInput.GamePlay.Dash.canceled += OnDash;

    }

    //Handles Movement and Animation
    private void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
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

    //Disables Movement Input Actions
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

        if (dashPressed && canDash && !knockBack.isBeingKnockedBack)
        {
            StartCoroutine(DashCoroutine());
        }

        if ((isIdle || isMoving) && (!isDashing && !isDamaged))
        {
            rb.velocity = moveVector * speed;
        }
    }

    //Handles Movement Input Actions
    private void OnMovement(InputAction.CallbackContext context)
    {
        isMoving = true;
        isIdle = false;

        moveVector = context.ReadValue<Vector2>();
        faceDirection = moveVector.normalized;
    }

    private void OnMovementCancel(InputAction.CallbackContext context)
    {
        isMoving = false;
        isIdle = true;

        moveVector = Vector2.zero;
    }

    /// <summary>
    /// Handles the animation based on the state of the player and the direction they are facing
    /// </summary>
    private void HandleAnimation()
    {
        if(isIdle)
        {
            animationControl.SetState(AnimationState.IDLE);
        }

        if(isMoving)
        {
            animationControl.SetState(AnimationState.MOVING);
        }

        if(isDashing)
        {
            animationControl.SetState(AnimationState.DASHING);
        }

        if(isAttacking)
        {
            animationControl.SetState(AnimationState.ATTACKING);
        }

        if(isDamaged)
        {
            animationControl.SetState(AnimationState.DAMAGED);
        }

        animationControl.PlayAnimation(faceDirection);
    }

    //Handles Dash Input Actions
    private void OnDash(InputAction.CallbackContext context)
    {
        isIdle = false;
        isMoving = false;

        dashPressed = context.ReadValueAsButton();
    }

    //Dash Coroutine set the rigidbody to the dashing power for a set amount of time
    private IEnumerator DashCoroutine()
    {
        //Handles Initial Dash
        DisableMovement();
        isMoving = false;
        isDashing = true;
        canDash = false;

        tr.emitting = true;
        rb.velocity = faceDirection * dashingPower;
        yield return new WaitForSeconds(dashingTime);

        //Handles Dash End
        EnableMovement();
        tr.emitting = false;
        isDashing = false;
        isMoving = true;
        rb.velocity = Vector2.zero;

        //Handles Dash Cooldown
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    public Vector2 GetMoveDir()
    {
        return moveVector;
    }

    //Handles Damage and Knockback
    public void Damage(int damage, Vector2 hitDirection, float hitForce, Vector2 constantForceDirection, float inputDirection)
    {
        isDamaged = true;
        isMoving = false;
        isIdle = false;
        isDashing = false;
        isAttacking = false;

        playerHealth.Damage(damage);
        knockBack.CallKnockback(hitDirection, hitForce, constantForceDirection, inputDirection);
    }

    private void SetIdleEvent()
    {
        isIdle = true;
        isMoving = false;
        isDashing = false;
        isAttacking = false;
        isDamaged = false;
    }
}
