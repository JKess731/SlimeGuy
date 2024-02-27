using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    public AnimState currentState;

    public Vector3 moveVector;
    private int directionX;
    private int directionY;

    public bool isMoving;
    private bool isBeingHit;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        ChangeState();
        
        switch (currentState)
        {
            case AnimState.IDLE_LEFT:
                animator.Play("Player_Idle_LFacing");
                break;
            case AnimState.IDLE_RIGHT:
                animator.Play("Player_Idle_RFacing");
                break;
            case AnimState.IDLE_UP:
                animator.Play("Player_Idle_UFacing");
                break;
            case AnimState.IDLE_DOWN:
                animator.Play("Player_Idle_DFacing");
                break;
            case AnimState.MOVE_LEFT:
                animator.Play("Player_Move_LFacing");
                break;
            case AnimState.MOVE_RIGHT:
                animator.Play("Player_Move_RFacing");
                break;
            case AnimState.MOVE_UP:
                animator.Play("Player_Move_UFacing");
                break;
            case AnimState.MOVE_DOWN:
                animator.Play("Player_Move_DFacing");
                break;
            case AnimState.HIT_LEFT_WHITE:
                animator.Play("Player_Hit_LWhite");
                break;
            case AnimState.HIT_RIGHT_WHITE:
                animator.Play("Player_Hit_RWhite");
                break;
            case AnimState.HIT_LEFT_RED:
                animator.Play("Player_Hit_LRed");
                break;
            case AnimState.HIT_RIGHT_RED:
                animator.Play("Player_Hit_RRed");
                break;
        }
    }

    /// <summary>
    /// Used to stop animations from snapping, using AnimtionEvents
    /// </summary>
    private void AnimationEventDoNothing()
    {
        // For animations to stop snapping
    }

    /// <summary>
    /// Changes the animation state of the player
    /// </summary>
    private void ChangeState()
    {
        if (isMoving)
        {

        }
    }

}

public enum AnimState
{
    IDLE_LEFT,
    IDLE_RIGHT,
    IDLE_UP,
    IDLE_DOWN,
    MOVE_LEFT,
    MOVE_RIGHT,
    MOVE_UP,
    MOVE_DOWN,
    HIT_LEFT_WHITE,
    HIT_RIGHT_WHITE,
    HIT_LEFT_RED,
    HIT_RIGHT_RED
}
