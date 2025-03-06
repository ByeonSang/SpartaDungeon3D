using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public PlayerState CurrentState { get; set; }

    public void Initialize(PlayerState startState)
    {
        CurrentState = startState;
        startState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState != null)
            CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}
