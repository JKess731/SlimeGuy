using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Edison Li

//Mandates the requirement of a characterController
[RequireComponent(typeof(CharacterController))]
public class characterMovement : MonoBehaviour
{
    //Player Controller, Inputs, Movement directions
    public CharacterController controller;
    public Vector2 input;
    public Vector2 moveDirection;

    public float speed;

    public Vector2 faceDirection;

    //Get CharacterController Component
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    //Calls controller.Move(Vector2D)
    private void Update()
    {
        //Handles Movement
        controller.Move(moveDirection * speed * Time.deltaTime);

        //Handles Rotation
        Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAngle,Vector3.forward);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        moveDirection = new Vector2(input.x, input.y);
    }
}
