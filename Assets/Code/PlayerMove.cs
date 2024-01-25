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
    private PlayerInput input = null;
    
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        input = new PlayerInput(); 
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * speed;
    }

    private void OnEnable()
    {
        input.Enable();
        input.GamePlay.Movement.performed += onMovement;
        input.GamePlay.Movement.canceled += onMovementCancel;
    }

    private void OnDisable()
    {
        input.Disable();
        input.GamePlay.Movement.performed -= onMovement;
        input.GamePlay.Movement.canceled -= onMovementCancel;
    }

    private void onMovement(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void onMovementCancel(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
}
