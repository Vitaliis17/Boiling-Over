using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TimeInteractionStateMachine : MonoBehaviour
{
    private Dictionary<Type, float> _interactionTimes;
    private Timer _timer;

    private Coroutine _coroutine;

    public event Action InteractionCompleted;

    private void Awake()
    {
        _interactionTimes = new Dictionary<Type, float>()
        {
            { typeof(Seat), 4f },
            { typeof(StayingPlace), 4f},
            { typeof(Player), 2f}
        };

        _timer = new Timer();
    }

    private void OnDisable()
        => StopCoroutine();

    public void Interact(IRemyInteractable interactable)
    {
        StopCoroutine();

        _coroutine = StartCoroutine(WaitInteractable(interactable));
    }

    private IEnumerator WaitInteractable(IRemyInteractable interactable)
    {
        float waitingTime = _interactionTimes[interactable.GetType()];

        yield return _timer.Wait(waitingTime);

        InteractionCompleted?.Invoke();
    }

    private void StopCoroutine()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}