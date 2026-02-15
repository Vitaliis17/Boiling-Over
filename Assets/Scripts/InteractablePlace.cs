using System.Collections;
using System;
using UnityEngine;

public class InteractablePlace : MonoBehaviour
{
    [SerializeField] private TimerData _timerData;
    [SerializeField] private States _state;

    private Coroutine _coroutine;

    public event Action<InteractablePlace> Releasing;

    public States State => _state;

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