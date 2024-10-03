using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                _animator.Play("Damage White");
            }

            if(_damageColor == DamagedColor.RED)
            {
                _animator.Play("Damage Red");
            }
        }

        if (state == Enum_State.DEAD)
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
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndMenuL");
    }
}

//Damaged Color Enum for Damaged Animation
public enum DamagedColor
{
    WHITE,
    RED
}

