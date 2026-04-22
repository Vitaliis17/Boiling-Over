using UnityEngine;
using R3;

[RequireComponent(typeof(Collider))]
public class WinChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private readonly Subject<Unit> _triggered = new();
    
    public Observable<Unit> Triggered => _triggered;

    private void Awake()
        => GetComponent<Collider>().isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer != _layerMask.value)
            return;

        _triggered.OnNext(Unit.Default);
    }

    private void OnDestroy()
        => _triggered?.Dispose();
}
