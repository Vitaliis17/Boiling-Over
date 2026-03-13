using System;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyData _data;

    public event Action<Key> Using;

    public void Interact()
    {
        Using?.Invoke(this);

        gameObject.SetActive(false);
    }

    public bool IsEqualName(string name)
        => _data.Name == name;
}