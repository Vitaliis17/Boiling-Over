using UnityEngine;
using System;
using System.Collections.Generic;

public class InteractingStateMachine
{
    private Dictionary<Type, MovementActions> _actions;

    public InteractingStateMachine()
    {
        _actions = new Dictionary<Type, MovementActions>()
        {
            { typeof(Seat), MovementActions.Sitting },
            { typeof(StayingPlace), MovementActions.Idle },
            { typeof(Player), MovementActions.Attack }
        };
    }

    public event Action<Quaternion> RotationChanged;
    public event Action<MovementActions> StateChanged;

    public void Interact(IRemyInteractable interactable)
    {
        interactable.Interact();

        StateChanged?.Invoke(_actions[interactable.GetType()]);
        RotationChanged?.Invoke(((MonoBehaviour)interactable).transform.rotation);
    }
}