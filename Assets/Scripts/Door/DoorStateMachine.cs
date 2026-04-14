using System;
using System.Collections.Generic;

public class DoorStateMachine
{
    private Dictionary<DoorStates, Action> _states;

    private DoorStates _currentState;

    public event Action Opened;
    public event Action Closed;

    public DoorStateMachine()
    {
        _states = new()
        {
            { DoorStates.Closed, InvokeClose },
            { DoorStates.Opened, InvokeOpen }
        };

        Reset();
    }

    public void ChangeState()
    {
        _currentState++;

        if ((int)_currentState == _states.Count)
        Reset();

        InvokeCurrentAction();
    }

    private void Reset()
    {
        const int MinIndex = 0;

        _currentState = MinIndex;
    }

    private void InvokeCurrentAction()
        => _states[_currentState]?.Invoke();

    private void InvokeOpen()
        => Opened?.Invoke();

    private void InvokeClose()
        => Closed?.Invoke();
}