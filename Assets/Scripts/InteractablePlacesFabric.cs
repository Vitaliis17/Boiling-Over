using System;
using System.Collections.Generic;

public class InteractablePlacesFabric
{
    private List<InteractablePlace> _places;

    public event Action<InteractablePlace> PlaceActivated;
    public event Action PlaceDeactivated;

    public void Initialize(InteractablePlace[] places)
    {
        _places = new List<InteractablePlace>();

        for (int i = 0; i < places.Length; i++)
            _places.Add(places[i]);

        ActivatePlace();
    }

    public void ActivatePlace()
    {
        InteractablePlace place = GetRandomPlace();

        if (place == null)
            return;

        place.gameObject.SetActive(true);
        _places.Remove(place);

        place.Releasing += Release;
        PlaceActivated?.Invoke(place);
    }

    public void Release(InteractablePlace place)
    {
        place.Releasing -= Release;

        _places.Add(place);
        place.gameObject.SetActive(false);

        PlaceDeactivated?.Invoke();
    }

    private InteractablePlace GetRandomPlace()
    {
        const int MinIndex = 0;
        const int MinElementAmount = 1;

        if (_places.Count < MinElementAmount)
            return null;

        if(_places.Count == MinIndex)
            return _places[MinIndex];

        int lastReleasedPlaceIndex = _places.Count - 1;
        int index = UnityEngine.Random.Range(MinIndex, lastReleasedPlaceIndex);

        return _places[index];
    }
}