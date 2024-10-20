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

    private Enum_AnimationState _state;                      //State Variables
    private KnockBack _knockBack;                   //Knockback Variables

    public Enum_AnimationState State { get => _state; set => _state = value; }
    public StatsSO playerStats { get => _playerStats; }

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

    private void Start()
    {
        UiManager.instance.UpdateHealthBar(_playerStats.GetStat(StatsEnum.HEALTH), _playerStats.GetStat(StatsEnum.MAXHEALTH));
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
        if (_state.Equals(Enum_AnimationState.DASHING))
        {
            Debug.Log("Player is dashing, no damage taken");
            return;
        }

        if (_playerStats.GetStat(StatsEnum.HEALTH) <= 0)
        {
            _state = Enum_AnimationState.DEAD;
            _playerController.DisableMovement();
            return;
        }

        _playerStats.SubtractStat(StatsEnum.HEALTH, damage);
        _knockBack.CallKnockback(hitDirection, hitForce, constantForceDirection);
        StartCoroutine(PlayerKnockback(0.3f));

        AudioManager.PlayOneShot(FmodEvents.instance.playerHurt, transform.position);
        UiManager.instance.UpdateHealthBar(_playerStats.GetStat(StatsEnum.HEALTH), _playerStats.GetStat(StatsEnum.MAXHEALTH));
    }

    public void SetState(Enum_AnimationState state)
    {
        _state = state;
    }

    private IEnumerator PlayerKnockback(float knockbackTime)
    {
        _state = Enum_AnimationState.DAMAGED;
        yield return new WaitForSeconds(knockbackTime);
        _state = Enum_AnimationState.IDLING;
    }
}
