using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using R3;

public class TimeInteractionStateMachine : IDisposable
{
    private readonly Dictionary<Type, float> _interactionTimes;
    private readonly Timer _timer;

    private readonly Subject<Unit> _interactionCompleted = new();
    private readonly Subject<Unit> _interactionOvered = new();

    private UniTask _task;

    private CancellationTokenSource _source;

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

    public Observable<Unit> InteractionCompleted => _interactionCompleted;
    public Observable<Unit> InteractionOvered => _interactionOvered;

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

        _interactionOvered.OnNext(Unit.Default);
    }

    private async UniTask WaitInteractable(IRemyInteractable interactable, CancellationToken token)
    {
        float waitingTime = _interactionTimes[interactable.GetType()];
        
        await _timer.WaitSeconds(waitingTime, token);
        
        _interactionCompleted.OnNext(Unit.Default);
        _interactionOvered.OnNext(Unit.Default);
    }

    public void Dispose()
    {
        _interactionCompleted?.Dispose();
        _interactionOvered?.Dispose();
    }

    private void Cancel()
    {
        _source?.Cancel();
        _source?.Dispose();

        _source = new();
    }
}