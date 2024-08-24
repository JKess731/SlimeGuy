using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    //References
    private Animator animator;

    //Debugging
    private PlayerState state;

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
        if (state == PlayerState.IDLE)
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

        if (state == PlayerState.MOVING)
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
 
        if (state == PlayerState.DAMAGED)
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

    /// <summary>
    /// Used to stop animations from snapping, using AnimtionEvents
    /// </summary>
    //private void AnimationEventDoNothing()
    //{
    //    //Do Nothing
    //}

    public void SetState(PlayerState state)
    {
        this.state = state;
    }
    
    //Debugging states purposes
    public void PrintStates()
    {
        Debug.Log("Current state:" + state);
    }

    //Damaged Color Enum for Damaged Animation
    public enum DamagedColor
    {
        WHITE,
        RED
    }
}