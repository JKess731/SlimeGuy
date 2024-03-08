using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    //References
    private Animator animator;

    //Animation States
    [HideInInspector] public bool isIdle;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isDashing;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isBeingHit;

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
        if (isIdle)
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

        if (isMoving)
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
 
        if (isBeingHit)
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
    private void AnimationEventDoNothing()
    {
        // For animations to stop snapping
    }

    public enum DamagedColor
    {
        WHITE,
        RED
    }
}