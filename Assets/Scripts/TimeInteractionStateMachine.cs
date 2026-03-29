using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class TimeInteractionStateMachine
{
    private Dictionary<Type, float> _interactionTimes;
    private Timer _timer;

    private UniTask _task;

    private CancellationTokenSource _source;

    public event Action InteractionCompleted;
    public event Action InteractionOvered;

    public TimeInteractionStateMachine()
    {
        _interactionTimes = new Dictionary<Type, float>()
        {
            { typeof(Seat), 4f },
            { typeof(StayingPlace), 4f},
            { typeof(Player), 2f}
        };

        _source = new();
        _timer = new();

        _task = UniTask.CompletedTask;
    }

    public void Interact(IRemyInteractable interactable)
    {
        StopInteraction();

        _task = WaitInteractable(interactable, _source.Token);
    }

    public void StopInteraction()
    {
        if (_task.Status != UniTaskStatus.Pending)
            return;

        Cancel();

        InteractionOvered?.Invoke();
    }

    private async UniTask WaitInteractable(IRemyInteractable interactable, CancellationToken token)
    {
        float waitingTime = _interactionTimes[interactable.GetType()];
        
        await _timer.WaitSeconds(waitingTime, token);
        
        InteractionCompleted?.Invoke();
        InteractionOvered?.Invoke();
    }

    private void Cancel()
    {
        _source?.Cancel();
        _source?.Dispose();

        _source = new();
    }
}