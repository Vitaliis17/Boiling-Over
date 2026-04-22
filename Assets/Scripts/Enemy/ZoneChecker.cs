using UnityEngine;
using R3;

[RequireComponent(typeof(BoxCollider))]
public class ZoneChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    private readonly Subject<Player> _playerFinded = new();
    private readonly Subject<Unit> _playerEscaped = new();

    private BoxCollider _collider;

    public Observable<Player> PlayerFinded => _playerFinded;
    public Observable<Unit> PlayerEscaped => _playerEscaped;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (1 << other.gameObject.layer != _targetLayers.value)
            return;

        Vector3 direction = (other.transform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, _collider.size.z) && hit.transform.TryGetComponent(out Player player))
            _playerFinded.OnNext(player);
    }


    private void OnTriggerExit(Collider other)
    {
        if (1 << other.gameObject.layer != _targetLayers.value)
            return;

        _playerEscaped.OnNext(Unit.Default);
    }

    private void OnDestroy()
    {
        _playerFinded?.Dispose();
        _playerEscaped?.Dispose();
    }
}