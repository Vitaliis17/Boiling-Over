using UnityEngine;
using System;

public class InteractingStateMachine
{
    private readonly TargetTypes _targetType;
    
    private InteractablePlace _currentPlace;

    public event Action<Vector3, TargetTypes> PlaceChanged;
    public event Action<Quaternion> RotationChanged;
    public event Action<MovementActions> StateChanged;

    public InteractingStateMachine()
        => _targetType = TargetTypes.Place;

    public void SetCurrentPlace(InteractablePlace place)
    {
        _currentPlace = place;

        PlaceChanged?.Invoke(place.transform.position, _targetType);
    }

    public void Interact()
    {
        _currentPlace.Interact();

        StateChanged?.Invoke(_currentPlace.State);
        RotationChanged?.Invoke(_currentPlace.transform.rotation);
    }
}