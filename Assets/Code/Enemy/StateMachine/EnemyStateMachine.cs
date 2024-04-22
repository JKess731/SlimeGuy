using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentEnemyState { get; set; }
    
    // Initialize the state machine with a starting state
    public void Initialize(EnemyState startingState) { 
        currentEnemyState = startingState;
        currentEnemyState.EnterState();
    }

    // Change the current state
    public void ChangeState(EnemyState newState)
    {
        currentEnemyState.ExitState();
        currentEnemyState = newState;
        currentEnemyState.EnterState();
    }
}
