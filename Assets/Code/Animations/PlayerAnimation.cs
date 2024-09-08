using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : AnimationControl
{
    //Damaged Color
    [SerializeField] private DamagedColor _damageColor;

    public override void PlayAnimation(Vector2 direction, Enum_State state)
    {
        base.PlayAnimation(direction, state);

        if (state == Enum_State.DAMAGED)
        {
            if(_damageColor == DamagedColor.WHITE)
            {
                _animator.Play("Damaged White");
            }

            if(_damageColor == DamagedColor.RED)
            {
                _animator.Play("Damaged Red");
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