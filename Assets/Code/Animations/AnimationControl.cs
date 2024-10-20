using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    //References
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Plays the animation based on the direction the player is facing and the state of the player
    /// </summary>
    /// <param name="direction"></param>
    public virtual void PlayAnimation(Vector2 direction, Enum_AnimationState state)
    {
        if (direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }


        if (state == Enum_AnimationState.IDLING)
        {
            if (direction.x != 0)
            {
                _animator.Play("Idle");
            }
            else
            {
                if (direction.y > 0)
                {
                    _animator.Play("Idle Up");
                }
                else if (direction.y < 0)
                {
                    _animator.Play("Idle Down");
                }
            }
        }

        if (state == Enum_AnimationState.MOVING)
        {
            if (direction.x != 0)
            {
                _animator.Play("Move");
            }
            else
            {
                if (direction.y > 0)
                {
                    _animator.Play("Move Up");
                }
                else if (direction.y < 0)
                {
                    _animator.Play("Move Down");
                }
            }
        }
    }

    #region Enum_State to String
    protected void Play(Enum_AnimationState state, Vector2 direction)
    {
        string stateString = state.ToString();
        stateString = stateString.Substring(0, 1).ToUpper() + stateString.Substring(1).ToLower();

        if (direction.x != 0)
        {
            _animator.Play(stateString);
        }
        else
        {
            if (direction.y > 0)
            {
                _animator.Play(stateString + " Up");
            }

            _animator.Play(stateString + " Down");
        }
    }
    #endregion
}