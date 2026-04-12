using UnityEngine;

public class InteractablePlace : MonoBehaviour, IRemyInteractable
{
    public MovementActions State { get; protected set; }

    public void Interact()
    {
    }
}