using System.Collections;
using System;
using UnityEngine;

public class InteractablePlace : MonoBehaviour, IRemyInteractable
{
    [SerializeField] private TimerData _timerData;

    private Coroutine _coroutine;

    public event Action<InteractablePlace> Releasing;

    public MovementActions State { get; protected set; }

    private void OnDisable()
        => StopCoroutine();

    public void Interact()
    {
        StopCoroutine();
        _coroutine = StartCoroutine(SetInteractTimer());
    }

    private IEnumerator SetInteractTimer()
    {
        yield return new WaitForSeconds(_timerData.GenerateTime());

        Releasing?.Invoke(this);
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}