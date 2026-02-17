using UnityEngine;
using System;

public class WinnerSetter : MonoBehaviour
{
    [SerializeField] private Transform _panel;

    public event Action Activated;

    public void Activate()
    {
        _panel.gameObject.SetActive(true);

        Activated?.Invoke();
    }
}