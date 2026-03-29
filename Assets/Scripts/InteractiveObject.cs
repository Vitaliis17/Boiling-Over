using UnityEngine;
using System;

public class InteractiveObject : MonoBehaviour, IInteractable
{
    private CustomeLayerMasks _mask;

    public event Action Interacting;

    private void Awake()
    {
        _mask = CustomeLayerMasks.Interactable;

        gameObject.layer = (int)_mask;
    }

    public void Interact()
        => Interacting?.Invoke();
}