using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _currentState;

    public void ChangeState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Execute();
    }
}
