using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class ZoneChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    private BoxCollider _collider;

    public event Action<Player> PlayerFinded;

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
            PlayerFinded?.Invoke(player);
    }
}