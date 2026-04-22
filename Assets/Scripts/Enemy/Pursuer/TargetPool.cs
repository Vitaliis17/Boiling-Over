public class TargetPool
{
    private readonly InteractablePlacesPool _placesPool;

    private IRemyInteractable _currentInteractableObject;

    public TargetPool(InteractablePlacesPool pool, InteractablePlace[] places)
    {
        _placesPool = pool;
        _placesPool.Initialize(places);
    }

    public IRemyInteractable Get()
    {
        if (_currentInteractableObject != null)
            Release();

        _currentInteractableObject = _placesPool.Get();

        return _currentInteractableObject;
    }

    public void Release()
    {
        if (_currentInteractableObject is InteractablePlace place)
            _placesPool.Release(place);

        _currentInteractableObject = null;
    }
}