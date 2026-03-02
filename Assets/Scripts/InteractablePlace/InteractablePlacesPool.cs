using System.Collections.Generic;

public class InteractablePlacesPool
{
    private List<InteractablePlace> _places;

    public void Initialize(InteractablePlace[] places)
    {
        _places = new List<InteractablePlace>();

        for (int i = 0; i < places.Length; i++)
            _places.Add(places[i]);
    }

    public InteractablePlace ActivatePlace()
    {
        InteractablePlace place = GetRandomPlace();

        if (place == null)
            return null;

        _places.Remove(place);

        return place;
    }

    public void Release(InteractablePlace place)
        => _places.Add(place);

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