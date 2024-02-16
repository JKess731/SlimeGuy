using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Jared Kessler
// Connection to the new Input System

// Code taken from:
// https://www.youtube.com/watch?v=UyUogO2DvwY&t=268s
/*
 * 
 * Feel Free to edit this code as needed
 * But do not break the game
 * 
 */
public class PlayerMove : MonoBehaviour
{

    public PlayerInput input = null;

    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    [SerializeField] private float speed = 10f;

    public bool isDashing;
    public Vector2 playerFaceDirection;

    // Animation States
    private AnimationControl animControl;
    public int directionX;
    public int directionY;
    private bool isWalking;

    private string IDLE_LEFT = "Player_Idle_LFacing";
    private string IDLE_RIGHT = "Player_IDle_RFacing";
    private string MOVE_LEFT = "Player_Move_LFacing";
    private string MOVE_RIGHT = "Player_Move_RFacing";
    private string MOVE_DOWN = "Player_Move_DFacing";
    private string MOVE_UP = "Player_Move_UFacing";

    private void Awake()
    {
        animControl = GetComponent<AnimationControl>();

        input = new PlayerInput();

        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        playerFaceDirection = moveVector.normalized;

        if (isDashing)
        {
            return;
        }

        rb.velocity = moveVector * speed;

    }

    private void OnEnable()
    {
        input.Enable();
        input.GamePlay.Movement.performed += OnMovement;
        input.GamePlay.Movement.canceled += OnMovementCancel;

    }

    private void OnDisable()
    {
        input.Disable();
        input.GamePlay.Movement.performed -= OnMovement;
        input.GamePlay.Movement.canceled -= OnMovementCancel;
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();

        if (moveVector.x > 0)
        {
            directionX = 1;
            animControl.ChangeAnimationState(MOVE_RIGHT);
        }
        else if (moveVector.x < 0)
        {
            directionX = -1;
            animControl.ChangeAnimationState(MOVE_LEFT);
        }
        else
        {
            directionX = 0;
            if (moveVector.y > 0)
            {
                directionY = 1;
                animControl.ChangeAnimationState(MOVE_UP);
            }
            else if (moveVector.y < 0)
            {
                directionY = 1;
                animControl.ChangeAnimationState(MOVE_DOWN);
            }
        }

    }

    private void OnMovementCancel(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;

        if (directionX > 0)
        {
            animControl.ChangeAnimationState(IDLE_RIGHT);
        }
        else if (directionX < 0)
        {
            animControl.ChangeAnimationState(IDLE_LEFT);
        }
        else
        {
            if (directionY > 0)
            {
                // IDLE_UP once done
                animControl.ChangeAnimationState(IDLE_RIGHT);
            }
            else if (directionY < 0)
            {
                // IDLE_DOWN once done
                animControl.ChangeAnimationState(IDLE_LEFT);
            }
        }

    }

    
}
