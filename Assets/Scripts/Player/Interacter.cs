using UnityEngine;

public class Interacter
{
    private readonly Transform _transform;

    public Interacter(Transform transform)
        => _transform = transform;

    public void Interacte(IInteractable component)
        => component.Interact();
}