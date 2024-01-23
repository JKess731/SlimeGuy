using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jared
// TEMPORARY MOVEMENT SCRIPT
// **************************
//   DELETE AFTER TESTING
// **************************

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float movementSpeed;
    [SerializeField] private float moveLimiter;

    public Vector2 moveDir;
    private float moveX;
    private float moveY;

    public bool isMoving = false;
    public int direction = 1;

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

    }

    private void FixedUpdate()
    {
        if (moveX != 0 && moveY != 0)
        {
            moveX *= moveLimiter;
            moveY *= moveLimiter;
        }

        rb.velocity = new Vector2(moveDir.x * movementSpeed, moveDir.y * movementSpeed);

        if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            isMoving = true;
        }
    }
}
