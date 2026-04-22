using UnityEngine;
using R3;

public class InteractiveObject : MonoBehaviour, IInteractable
{
    private readonly Subject<Unit> _interacting = new();
    private CustomeLayerMasks _mask;

    public Observable<Unit> Interacting => _interacting;

    private void Awake()
    {
        _mask = CustomeLayerMasks.Interactable;

        gameObject.layer = (int)_mask;
    }

    private void OnDestroy()
        => _interacting?.Dispose();

    public void Interact()
        => _interacting.OnNext(Unit.Default);
}