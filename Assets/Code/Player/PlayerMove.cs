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
    private Animator animator;

    public PlayerInput input = null;
    
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    [SerializeField] private float speed = 10f;

    public bool isDashing;
    public Vector2 playerFaceDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();

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

    private void AnimationEventDoNothing()
    {
        // For animations to stop snapping
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();

        animator.SetFloat("Direction", moveVector.x);
    }

    private void OnMovementCancel(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;

        animator.SetFloat("Direction", 0);
    }

    
}
