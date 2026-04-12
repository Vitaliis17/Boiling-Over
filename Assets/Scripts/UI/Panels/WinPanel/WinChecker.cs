using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class WinChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Collider _collider;

    public event Action PlayerTriggered;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer != _layerMask.value)
            return;

        PlayerTriggered?.Invoke();
    }
}
