using System;
using UnityEngine;

public class InteractablePlace : MonoBehaviour
{
    [SerializeField] private States State;

    public event Action<States> StartInteracting;
    public event Action<Quaternion> ContinueInteracting;

    public void Interact()
    {
        StartInteracting?.Invoke(State);
        ContinueInteracting?.Invoke(transform.rotation);
    }
}