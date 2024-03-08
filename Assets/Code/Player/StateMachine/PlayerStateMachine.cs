using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //References
    PlayerInput playerInput;
    AnimationControl animationControl;
    Rigidbody2D rigidBody;

    //Movement Variables
    private Vector2 moveVector = Vector2.zero;
    public Vector2 faceDirection;

    //Animation States
    bool isIdle;
    bool isMoving;
    bool isDashing;
    bool isAttacking;

    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        //Set up initial references
        animationControl = GetComponent<AnimationControl>();
        playerInput = new PlayerInput();
        rigidBody = GetComponent<Rigidbody2D>();

        //Set up input actions
        playerInput.GamePlay.Movement.performed += OnMovement;
        playerInput.GamePlay.Movement.canceled += OnMovementCancel;

    }

    //Handles Movement and Animation
    private void FixedUpdate()
    {
        HandleAnimation();
        HandleMovement();
    }

    //Enables Input Actions
    private void OnEnable()
    {
        playerInput.GamePlay.Enable();
    }

    //Disables Input Actions
    private void OnDisable()
    {
        playerInput.GamePlay.Disable();
    }

    //Handles Movement
    private void HandleMovement()
    {
        rigidBody.velocity = moveVector * speed;
    }

    //Handles Movement Input Actions
    private void OnMovement(InputAction.CallbackContext value)
    {
        isMoving = true;
        isIdle = false;

        moveVector = value.ReadValue<Vector2>();
        faceDirection = moveVector.normalized;
    }

    //Handles Movement Cancel Input Actions
    private void OnMovementCancel(InputAction.CallbackContext value)
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
}
