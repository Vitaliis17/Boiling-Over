using UnityEngine;
using System;

public class WorkingStateMachine
{
    private InteractablePlace[] _places;

    private InteractablePlace _currentPlace;

    public Action<Vector3> PlaceChanged;

    public void SetRandomCurrentPlace()
    {
        const int MinIndex = 0;
        const int MinElementAmount = 2;

        if (_places.Length < MinElementAmount)
            return;

        int index = UnityEngine.Random.Range(MinIndex, _places.Length);

        if(_currentPlace == _places[index])
            index = index == (_places.Length - 1) ? index-- : index++;

        ChangePlace(_places[index]);
    }

    public void Initialize(InteractablePlace[] places)
    {
        _places = new InteractablePlace[places.Length];

        for(int i = 0; i < places.Length; i++)
            _places[i] = places[i];

        SetRandomCurrentPlace();
    }

    private void ChangePlace(InteractablePlace place)
    {
        _currentPlace = place;
        
        if (place is Component component)
            PlaceChanged?.Invoke(component.transform.position);
    }
}