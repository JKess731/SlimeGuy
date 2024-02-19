using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {

        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;

    }
    private void AnimationEventDoNothing()
    {
        // For animations to stop snapping
    }
}
