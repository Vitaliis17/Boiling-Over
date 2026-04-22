using UnityEngine;
using R3;

public class WinnerSetter : MonoBehaviour
{
    [SerializeField] private Transform _panel;

    private readonly Subject<Unit> _activated = new();

    public Observable<Unit> Activated => _activated;

    private void OnDestroy()
        => _activated?.Dispose();

    public void Activate()
    {
        _panel.gameObject.SetActive(true);

        _activated.OnNext(Unit.Default);
    }
}