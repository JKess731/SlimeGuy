using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Edison Li

//Mandates the requirement of a characterController
[RequireComponent(typeof(CharacterController))]
public class characterMovement : MonoBehaviour
{
    //Player Controller, Inputs, Directions
    public CharacterController controller;
    public Vector2 input;
    public Vector2 direction;

    public float speed;

    //Get CharacterController Component
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    //Calls controller.Move(Vector2D)
    private void Update()
    {
        controller.Move(direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector2(input.x, input.y);
    }
}
