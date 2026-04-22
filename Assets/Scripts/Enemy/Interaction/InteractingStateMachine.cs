using UnityEngine;
using System;
using System.Collections.Generic;
using R3;

public class InteractingStateMachine : IDisposable
{
    private readonly Subject<Quaternion> _rotationChanged = new();
    private readonly Subject<MovementActions> _stateChanged = new();

    private readonly Dictionary<Type, MovementActions> _actions;

    public InteractingStateMachine()
    {
        _actions = new Dictionary<Type, MovementActions>()
        {
            { typeof(Seat), MovementActions.Sitting },
            { typeof(StayingPlace), MovementActions.Idle },
            { typeof(Player), MovementActions.Attack }
        };
    }

    public Observable<Quaternion> RotationChanged => _rotationChanged;
    public Observable<MovementActions> StateChanged => _stateChanged;

    public void Interact(IRemyInteractable interactable)
    {
        interactable.Interact();

        _stateChanged.OnNext(_actions[interactable.GetType()]);
        _rotationChanged.OnNext(((MonoBehaviour)interactable).transform.rotation);
    }

    public void Dispose()
    {
        _rotationChanged?.Dispose();
        _stateChanged?.Dispose();
    }
}