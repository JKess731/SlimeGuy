using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //References
    private PlayerStats playerStats;
    private PlayerController controller;
    private AnimationControl animationControl;

    //Knockback Variables
    private KnockBack knockBack;

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
        animationControl.PlayAnimation(controller.faceDirection, controller.state);
    }

    //Handles Damage and Knockback
    public void Damage(int damage, Vector2 hitDirection, float hitForce, Vector2 constantForceDirection)
    {
        playerStats.Damage(damage);
        knockBack.CallKnockback(hitDirection, hitForce, constantForceDirection);
        AudioManager.instance.PlayOneShot(FmodEvents.instance.playerHurt, transform.position);
    }

    private void SetIdleEvent()
    {
        controller.SetIdle();
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
