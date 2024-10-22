using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimation : AnimationControl
{
    public override void PlayAnimation(Vector2 direction, Enum_AnimationState state)
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
            _animator.Play("Idle");
        }

        if (state == Enum_AnimationState.MOVING)
        {
            _animator.Play("Move");
        }

        if (state == Enum_AnimationState.TELEPORTING)
        {
            _animator.Play("Teleport");
        }

        if (state == Enum_AnimationState.ATTACKING)
        {
            _animator.Play("Attack");
        }

        if (state == Enum_AnimationState.DAMAGED)
        {
            _animator.Play("Damaged");
        }

        if (state == Enum_AnimationState.DEAD)
        {
            _animator.Play("Dead");
        }
    }
}
