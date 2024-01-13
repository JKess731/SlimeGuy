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
    private CharacterController controller;
    private Vector2 input;
    private Vector2 direction;

    [SerializeField] private float speed;

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
        Debug.Log(input);
    }
}
