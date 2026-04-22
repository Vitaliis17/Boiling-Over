using System;
using System.Collections.Generic;
using R3;

public class DoorStateMachine : IDisposable
{
    private readonly Subject<Unit> _opened = new();
    private readonly Subject<Unit> _closed = new();

    private readonly Dictionary<DoorStates, Action> _states;

    private DoorStates _currentState;

    public DoorStateMachine()
    {
        _states = new()
        {
            { DoorStates.Closed, InvokeClose },
            { DoorStates.Opened, InvokeOpen }
        };

        Reset();
    }

    public Observable<Unit> Opened => _opened;
    public Observable<Unit> Closed => _closed;

    public void Dispose()
    {
        _opened?.Dispose();
        _closed?.Dispose();
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
        => _opened.OnNext(Unit.Default);

    private void InvokeClose()
        => _closed.OnNext(Unit.Default);
}