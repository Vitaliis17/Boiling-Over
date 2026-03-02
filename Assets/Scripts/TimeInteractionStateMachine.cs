using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TimeInteractionStateMachine : MonoBehaviour
{
    private Dictionary<Type, float> _interactionTimes;
    private Timer _timer;

    private Coroutine _coroutine;
    private bool _isInteracting;

    public event Action InteractionCompleted;
    public event Action InteractionOvered;

    private void Awake()
    {
        _interactionTimes = new Dictionary<Type, float>()
        {
            { typeof(Seat), 4f },
            { typeof(StayingPlace), 4f},
            { typeof(Player), 2f}
        };

        _timer = new Timer();
        _isInteracting = false;
    }

    private void OnDisable()
        => StopInteraction();

    public void Interact(IRemyInteractable interactable)
    {
        StopInteraction();

        _coroutine = StartCoroutine(WaitInteractable(interactable));
    }

    public void StopInteraction()
    {
        if (_isInteracting == false)
            return;

        _isInteracting = false;
        StopCoroutine();

        InteractionOvered?.Invoke();
    }

    private IEnumerator WaitInteractable(IRemyInteractable interactable)
    {
        _isInteracting = true;
        float waitingTime = _interactionTimes[interactable.GetType()];

        yield return _timer.Wait(waitingTime);

        InteractionCompleted?.Invoke();
        InteractionOvered?.Invoke();

        _isInteracting = false;
    }

    private void StopCoroutine()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}