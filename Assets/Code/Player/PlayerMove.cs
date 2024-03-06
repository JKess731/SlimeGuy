using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

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

    //Serialized Fields
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float speed = 10f;

    // Components

    //---Movement---
    private Rigidbody2D rb;
    private Vector2 moveVector = Vector2.zero;
    public Vector2 faceDirection;
    private bool isMoving;
    private TrailRenderer trail;

    //---Dash---
    public bool isDashing = false;
    private bool canDash = true;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    //---Aninmation Buffer---
    private AnimationControl animControl;
    public int directionX;
    public int directionY;

    //---Input Buffer---
    public InputBuffer inputBuffer = new InputBuffer();

    private void Awake()
    {
        animControl = GetComponent<AnimationControl>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        inputReader.movementEvent += HandleMovement;
        inputReader.movementEventCancel += HandleMovementCancel;

        inputReader.dashEvent += HandleDash;
        inputReader.dashEventCancel += HandDashCancel;
    }

    private void FixedUpdate()
    {
        // Update the input buffer
        inputBuffer.Update();

        Move();
        Dash();

        // Set the player's face direction
        faceDirection = moveVector.normalized;
    }

    private void Dash()
    {
        if (isDashing)
        {    
            canDash = false;
            isDashing = true;
            rb.velocity = faceDirection * dashingPower;
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
            isDashing = false;
            canDash = true;
        }
    }

    //Move the player
    private void Move()
    {
        rb.velocity = moveVector * speed;

        if (isMoving)
        {
            animControl.isMoving = true;

            if (moveVector.x > 0)
            {
                directionX = 1;
                animControl.currentState = AnimState.MOVE_RIGHT;
            }
            else if (moveVector.x < 0)
            {
                directionX = -1;
                animControl.currentState = AnimState.MOVE_LEFT;
            }
            else
            {
                directionX = 0;
                if (moveVector.y > 0)
                {
                    directionY = 1;
                    animControl.currentState = AnimState.MOVE_UP;
                }
                else if (moveVector.y < 0)
                {
                    directionY = -1;
                    animControl.currentState = AnimState.MOVE_DOWN;
                }
            }
        }
        else
        {
            if (directionX > 0)
            {
                animControl.currentState = AnimState.IDLE_RIGHT;
            }
            else if (directionX < 0)
            {
                animControl.currentState = AnimState.IDLE_LEFT;
            }
            else
            {
                if (directionY > 0)
                {
                    animControl.currentState = AnimState.IDLE_UP;
                }
                else if (directionY < 0)
                {
                    animControl.currentState = AnimState.IDLE_DOWN;
                }
            }
        }
    }

    //Subscribe to the input reader movement event
    private void HandleMovement(Vector2 dir)
    {
        moveVector = dir;
        isMoving = true;
    }

    //Subscribe to the input reader movement cancel event
    private void HandleMovementCancel()
    {
        moveVector = Vector2.zero;
        isMoving = false;
    }

    //Subscribe to the input reader dash event
    private void HandleDash()
    {
        isDashing = true;
    }

    private void HandDashCancel()
    {
        isDashing = false;
    }

    //Disable movement event by unsubscribing from the input reader movement event
    public void DisableMovemnt()
    {
        moveVector = Vector2.zero;
        inputReader.movementEvent -= HandleMovement;
        inputReader.movementEventCancel -= HandleMovementCancel;
    }

    //Enable the movement event by resubscribing to the input reader movement event
    public void EnableMovemnt()
    {
        inputReader.movementEvent += HandleMovement;
        inputReader.movementEventCancel += HandleMovementCancel;
    }
}
