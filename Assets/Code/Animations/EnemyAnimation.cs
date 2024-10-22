using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AnimationControl
{
    public override void PlayAnimation(Vector2 direction, Enum_State state)
    {
        if (direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        if(state == Enum_State.IDLING)
        {
            _animator.Play("Idle");
        }

        if(state == Enum_State.MOVING)
        {
            _animator.Play("Move");
        }

        if(state == Enum_State.ATTACKING)
        {
            _animator.Play("Attack");
        }

        if(state == Enum_State.RANGEDATTACK)
        {
            _animator.Play("Throw");
        }

        if(state == Enum_State.DAMAGED)
        {
            _animator.Play("Damaged");
        }

        if(state == Enum_State.DEAD)
        {
            _animator.Play("Dead");
        }
    }
}