using UnityEngine;

public class Interacter : MonoBehaviour
{
    public void Interact()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, 1 << (int)CustomeLayerMasks.Interactable))
        {
            if (hit.transform.TryGetComponent(out IInteractable component))
                component.Interact();
        }
    }
}