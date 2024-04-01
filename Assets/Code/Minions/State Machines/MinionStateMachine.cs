using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStateMachine
{
    public MinionState currentState { get; set; }
    public GameObject target { get; set; }
    
    public void Initialize(MinionState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(MinionState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
