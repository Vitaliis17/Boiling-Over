using UnityEngine;
using System;

public class WorkingStateMachine
{
    private InteractablePlace _currentPlace;

    public event Action<Vector3> PlaceChanged;
    public event Action<Quaternion> RotationChanged;
    public event Action<States> StateChanged;

    public void SetCurrentPlace(InteractablePlace place)
    {
        _currentPlace = place;

        PlaceChanged?.Invoke(place.transform.position);
    }

    public void Interact()
    {
        _currentPlace.Interact();

        StateChanged?.Invoke(_currentPlace.State);
        RotationChanged?.Invoke(_currentPlace.transform.rotation);
    }
}