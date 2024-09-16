using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //References
    [SerializeField] private StatsSO _playerStats;
    private PlayerAnimation _animationControl;
    private PlayerController _playerController;

    private Enum_State _state;                      //State Variables
    private KnockBack _knockBack;                   //Knockback Variables

    public Enum_State State { get => _state; set => _state = value; }

    private void Awake()
    {
        //Set up initial references
        _playerStats = Instantiate(_playerStats);
        _playerStats.Initialize();

        //Set up initial references
        _animationControl = GetComponent<PlayerAnimation>();
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
        _animationControl.PlayAnimation(_playerController.FaceDirection, _state);
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
