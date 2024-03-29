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

    [SerializeField] private float dashingPower = 20f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;

    //Animation States
    bool isIdle = false;
    bool isMoving = false;
    bool isDashing = false;
    bool isAttacking = false;

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
        if(knockBack.isBeingKnockedBack)
        {
            return;
        }

        if (dashPressed && canDash && !knockBack.isBeingKnockedBack)
        {
            isDashing = true;
            StartCoroutine(DashCoroutine());
        }

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
        if (isDashing)
        {
            return;
        }

        rb.velocity = moveVector * speed;
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
        animationControl.isMoving = isMoving;
        animationControl.isIdle = isIdle;
        animationControl.isDashing = isDashing;
        animationControl.isAttacking = isAttacking;

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

    //Handles Knockback
    public void Knockback(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockBack.CallKnockback(hitDirection, constantForceDirection, inputDirection);
    }

    public Vector2 GetMoveDir()
    {
        return moveVector;
    }

    public void Damage(int damage)
    {
        playerHealth.Damage(damage);
    }
}
