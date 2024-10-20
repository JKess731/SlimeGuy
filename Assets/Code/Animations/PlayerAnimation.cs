using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : AnimationControl
{
    //Damaged Color
    [SerializeField] private DamagedColor _damageColor;

    public override void PlayAnimation(Vector2 direction, Enum_AnimationState state)
    {
        base.PlayAnimation(direction, state);

        if (state == Enum_AnimationState.DAMAGED)
        {
            if(_damageColor == DamagedColor.WHITE)
            {
                _animator.Play("Damage White");
            }

            if(_damageColor == DamagedColor.RED)
            {
                _animator.Play("Damage Red");
            }
        }

        if (state == Enum_AnimationState.DEAD)
        {
            _animator.Play("Dead");
        }
    }

    public void GoToEnd()
    {
        StartCoroutine(EndScreen());
    }

    private IEnumerator EndScreen()
    {
        AudioManager.instance.StopNiko();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndMenu");
    }
}

//Damaged Color Enum for Damaged Animation
public enum DamagedColor
{
    WHITE,
    RED
}

