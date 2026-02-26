using UnityEngine;
using System.Collections.Generic;
using System;

public class TargetStateMachine
{
    private readonly Dictionary<Type, MovementActions> _movementActions;

    private IRemyInteractable _currentInteractableObject;

    public event Action<Vector3> TargetChanged;
    public event Action TargetSetted;
    public event Action<MovementActions> MovementStarted;

    public event Action<IRemyInteractable> Transferring;
    public event Action<InteractablePlace> Releasing;

    public event Func<InteractablePlace> CurrentObjectChanging;

    public TargetStateMachine()
    {
        _movementActions = new Dictionary<Type, MovementActions>()
        {
            { typeof(Player), MovementActions.Running },
            { typeof(Seat), MovementActions.Walking },
            { typeof(StayingPlace), MovementActions.Walking }
        };
    }

    public void SetTarget(IRemyInteractable interactable)
    {
        ReleaseInteractableObject();

        _currentInteractableObject = interactable;

        InvokeTargetEvents();
    }

    public void GetInteractableObject()
    {
        if (_currentInteractableObject != null && _currentInteractableObject is InteractablePlace)
            ReleaseInteractableObject();

        InteractablePlace place = CurrentObjectChanging?.Invoke();
        place.InteractionOvered += ReleaseAfterInteraction;

        _currentInteractableObject = place;

        InvokeTargetEvents();
    }

    public void TransferCurrentObject()
        => Transferring?.Invoke(_currentInteractableObject);

    private void ReleaseInteractableObject()
    {
        if (_currentInteractableObject is InteractablePlace place)
        {
            place.InteractionOvered -= ReleaseAfterInteraction;
            Releasing?.Invoke(place);
        }
    }

    private void ReleaseAfterInteraction()
    {
        ReleaseInteractableObject();

        GetInteractableObject();
    }

    private void InvokeTargetEvents()
    {
        TargetSetted?.Invoke();
        TargetChanged?.Invoke(((MonoBehaviour)_currentInteractableObject).transform.position);
        MovementStarted?.Invoke(_movementActions[_currentInteractableObject.GetType()]);
    }
}