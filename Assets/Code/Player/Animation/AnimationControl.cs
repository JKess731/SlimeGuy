using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    //References
    private Animator animator;

    //Animation States
    private AnimationState currentState;

    //Damaged Color
    [SerializeField] private DamagedColor damageColor;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Plays the animation based on the direction the player is facing and the state of the player
    /// </summary>
    /// <param name="direction"></param>
    public void PlayAnimation(Vector2 direction)
    {
        if (currentState == AnimationState.IDLE)
        {
            if (direction.y > 0)
            {
                animator.Play("Player_Idle_UFacing");
            }
            else if (direction.y < 0)
            {
                animator.Play("Player_Idle_DFacing");
            }
            else if (direction.x > 0)
            {
                animator.Play("Player_Idle_RFacing");
            }
            else if (direction.x < 0)
            {
                animator.Play("Player_Idle_LFacing");
            }
            
        }

        if (currentState == AnimationState.MOVING)
        {
            if (direction.y > 0)
            {
                animator.Play("Player_Move_UFacing");
            }
            else if (direction.y < 0)
            {
                animator.Play("Player_Move_DFacing");
            }
            else if (direction.x > 0)
            {
                animator.Play("Player_Move_RFacing");
            }
            else if (direction.x < 0)
            {
                animator.Play("Player_Move_LFacing");
            }
            
        }
 
        if (currentState == AnimationState.DAMAGED)
        {
            if(damageColor == DamagedColor.WHITE)
                {
                if (direction.x > 0)
                {
                    animator.Play("Player_Hit_RWhite");
                }
                else
                {
                    animator.Play("Player_Hit_LWhite");
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    animator.Play("Player_Hit_RRed");

                }
                else
                {
                    animator.Play("Player_Hit_LRed");
                }
            }
        }
    }

    public void SetState(AnimationState state)
    {
        currentState = state;
    }
    /// <summary>
    /// Used to stop animations from snapping, using AnimtionEvents
    /// </summary>
    private void AnimationEventDoNothing()
    {
        //Do Nothing
    }
    
    //Debugging states purposes
    public void printStates()
    {
        Debug.Log("Current state:" + currentState);
    }

    //Damaged Color Enum for Damaged Animation
    public enum DamagedColor
    {
        WHITE,
        RED
    }
}