using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //References
    [SerializeField] private Stats _playerStats;
    private AnimationControl _animationControl;
    private PlayerController _playerController;

    private Enum_State _state;
    //Knockback Variables
    private KnockBack _knockBack;

    public Enum_State State { get => _state; set => value(); }

    private void Awake()
    {
        //Set up initial references
        _playerStats.Initialize();

        //Set up initial references
        _animationControl = GetComponent<AnimationControl>();
        _playerController = GetComponent<PlayerController>();
        _knockBack = GetComponent<KnockBack>();
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
        _animationControl.PlayAnimation(_playerController.faceDirection, _state);
    }

    //Handles Damage and Knockback
    public void Damage(int damage, Vector2 hitDirection, float hitForce, Vector2 constantForceDirection)
    {
        if (_playerStats.GetStat(StatsEnum.HEALTH) <= 0)
        {
            return;
        }

        _playerStats.SubtractStat(StatsEnum.HEALTH, damage);
        _knockBack.CallKnockback(hitDirection, hitForce, constantForceDirection);
        AudioManager.instance.PlayOneShot(FmodEvents.instance.playerHurt, transform.position);
    }

    public void SetState(Enum_State state)
    {
        _state = state;
    }
}
