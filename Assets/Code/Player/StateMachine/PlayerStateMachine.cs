using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //References
    private PlayerStats playerStats;
    [SerializeField] private PlayerController controller;
    private AnimationControl animationControl;

    //Dash Variables
    private bool canDash = true;
    private bool dashPressed;

    //Knockback Variables
    private KnockBack knockBack;

    //Dash Variables
    [Header("Dash Variables")]
    [SerializeField] private float dashingPower = 20f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;

    //Animation States
    
    [SerializeField] private bool isIdle = false;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isDamaged = false;

    private float speed;

    private void Awake()
    {
        //Not great
        playerStats = GetComponent<PlayerStats>();
        controller = GetComponent<PlayerController>();

        //Set up initial references
        animationControl = GetComponent<AnimationControl>();

        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        //speed = PlayerStats.instance.speed;
        speed = 10;
    }

    //Handles Movement and Animation
    private void FixedUpdate()
    {
        HandleAnimation();
    }

    //Disables Movement Input Actions

    /// <summary>
    /// Handles the animation based on the state of the player and the direction they are facing
    /// </summary>
    private void HandleAnimation()
    {
        if(isIdle)
        {
            animationControl.SetState(AnimationState.IDLE);
        }

        if(isMoving)
        {
            animationControl.SetState(AnimationState.MOVING);
        }

        if(isDashing)
        {
            animationControl.SetState(AnimationState.DASHING);
        }

        if(isAttacking)
        {
            animationControl.SetState(AnimationState.ATTACKING);
        }

        if(isDamaged)
        {
            animationControl.SetState(AnimationState.DAMAGED);
        }

        animationControl.PlayAnimation(controller.faceDirection);
    }

    //Handles Damage and Knockback
    public void Damage(int damage, Vector2 hitDirection, float hitForce, Vector2 constantForceDirection)
    {
        isDamaged = true;
        isMoving = false;
        isIdle = false;
        isDashing = false;
        isAttacking = false;

        playerStats.Damage(damage);
        knockBack.CallKnockback(hitDirection, hitForce, constantForceDirection);
        AudioManager.instance.PlayOneShot(FmodEvents.instance.playerHurt, transform.position);
    }

    private void SetIdleEvent()
    {
        isIdle = true;
        isMoving = false;
        isDashing = false;
        isAttacking = false;
        isDamaged = false;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
