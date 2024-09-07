using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    //References
    protected Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Plays the animation based on the direction the player is facing and the state of the player
    /// </summary>
    /// <param name="direction"></param>
    

    public virtual void PlayAnimation(Vector2 direction, Enum_State state)
    {
        if (state == Enum_State.IDLE)
        {
            if (direction.y > 0)
            {
                _animator.Play("Idle Up");
            }
            else if (direction.y < 0)
            {
                _animator.Play("Idle Down");
            }
            else if (direction.x > 0)
            {
                _animator.Play("Idle Right");
            }
            else if (direction.x < 0)
            {
                _animator.Play("Idle Left");
            }
            
        }

        if (state == Enum_State.MOVING)
        {
            if (direction.y > 0)
            {
                _animator.Play("Move Up");
            }
            else if (direction.y < 0)
            {
                _animator.Play("Move Down");
            }
            else if (direction.x > 0)
            {
                _animator.Play("Move Right");
            }
            else if (direction.x < 0)
            {
                _animator.Play("Move Left");
            }
            
        }
    }
}