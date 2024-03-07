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

    [SerializeField] private float speed = 10f;

    private Vector2 moveVector = Vector2.zero;
    public Vector2 faceDirection;

    bool isIdle;
    bool isMoving;
    bool isDashing;
    bool isAttacking;

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

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void OnEnable()
    {
        playerInput.GamePlay.Enable();
    }

    private void OnDisable()
    {
        playerInput.GamePlay.Disable();
    }

    private void HandleMovement()
    {
        rigidBody.velocity = moveVector * speed;
        faceDirection = moveVector.normalized;
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        isMoving = true;
        isIdle = false;

        moveVector = value.ReadValue<Vector2>();
        Debug.Log("Moving");
        Debug.Log(moveVector);
    }

    private void OnMovementCancel(InputAction.CallbackContext value)
    {
        isMoving = false;
        isIdle = true;
        moveVector = Vector2.zero;

        Debug.Log("Not Moving");
        Debug.Log(moveVector);
    }

    private void HandleAnimation()
    {
        animationControl.isMoving = isMoving;
        animationControl.isIdle = isIdle;
        animationControl.isDashing = isDashing;
        animationControl.isAttacking = isAttacking;

        if (moveVector.x > 0)
        {
            animationControl.currentState = AnimState.MOVE_RIGHT;
        }
        else if (moveVector.x < 0)
        {
            animationControl.currentState = AnimState.MOVE_LEFT;
        }
        else
        {
            if (moveVector.y > 0)
            {
                animationControl.currentState = AnimState.MOVE_UP;
            }
            else if (moveVector.y < 0)
            {
                animationControl.currentState = AnimState.MOVE_DOWN;
            }
        }

        if (faceDirection.x > 0)
        {
            animationControl.currentState = AnimState.IDLE_RIGHT;
        }
        else if (faceDirection.x < 0)
        {
            animationControl.currentState = AnimState.IDLE_LEFT;
        }

        if (faceDirection.y > 0)
        {
            animationControl.currentState = AnimState.IDLE_UP;
        }
        else if (faceDirection.y < 0)
        {
            animationControl.currentState = AnimState.IDLE_DOWN;
        }
    }
}
