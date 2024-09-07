using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : AnimationControl
{
    //Damaged Color
    [SerializeField] private DamagedColor _damageColor;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void PlayAnimation(Vector2 direction, Enum_State state)
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

        if(state == Enum_State.DAMAGED)
        {
            if (_damageColor == DamagedColor.RED)
            {
                if (direction.x > 0)
                {
                    _animator.Play("Damage Right Red");
                }
                else if (direction.x < 0)
                {
                    _animator.Play("Damage Left Red");
                }
            }

            if (_damageColor == DamagedColor.WHITE)
            {
                if (direction.x > 0)
                {
                    _animator.Play("Damage Right White");
                }
                else if (direction.x < 0)
                {
                    _animator.Play("Damage Left White");
                }
            }
        }
    }
}

//Damaged Color Enum for Damaged Animation
public enum DamagedColor
{
    WHITE,
    RED
}