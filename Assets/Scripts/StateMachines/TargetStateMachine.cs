using UnityEngine;
using System.Collections.Generic;
using System;

public class TargetStateMachine
{
    private readonly Dictionary<Type, int> _typePriorities;
    private readonly Dictionary<Type, MovementActions> _movementActions;

    private IRemyInteractable _currentInteractableObject;

    public event Action<Vector3> TargetChanged;
    public event Action TargetSetted;
    public event Action<MovementActions> MovementStarted;

    public event Action<IRemyInteractable> Transferring;

    public TargetStateMachine()
    {
        _typePriorities = new Dictionary<Type, int>()
        {
            { typeof(Player), 1},
            { typeof(InteractablePlace), 2}
        };

        _movementActions = new Dictionary<Type, MovementActions>()
        {
            { typeof(Player), MovementActions.Running },
            { typeof(InteractablePlace), MovementActions.Walking }
        };

        _currentInteractableObject = null;
    }

    public void SetTarget(IRemyInteractable interactable)
    {
        if (HaveSwapPriority(interactable) == false)
            return;

        _currentInteractableObject = interactable;

        TargetSetted?.Invoke();
        TargetChanged?.Invoke(((MonoBehaviour)interactable).transform.position);
        MovementStarted?.Invoke(_movementActions[interactable.GetType()]);
    }

    public void TransferCurrentObject()
        => Transferring?.Invoke(_currentInteractableObject);

    private bool HaveSwapPriority(IRemyInteractable interactable)
        => interactable == null || _typePriorities[interactable.GetType()] <= _typePriorities[_currentInteractableObject.GetType()];
}