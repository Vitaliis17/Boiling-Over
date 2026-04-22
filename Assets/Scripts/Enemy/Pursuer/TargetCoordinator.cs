using R3;
using System;
using UnityEngine;

public class TargetCoordinator : IDisposable
{
    private readonly TargetPool _targetStateMachine;
    private readonly PriorityTargetsContainer _targetSetter;

    private readonly Subject<Vector3> _targetSetted = new();
    private readonly Subject<Type> _interactableObjectChanged = new();
    private readonly Subject<IRemyInteractable> _transferring = new();

    private IRemyInteractable _currentTarget;
 
    public TargetCoordinator(TargetPool stateMachine, PriorityTargetsContainer setter)
    {
        _targetStateMachine = stateMachine;
        _targetSetter = setter;
    }

    public Observable<Vector3> TargetSetted => _targetSetted;
    public Observable<Type> InteractableObjectChanged => _interactableObjectChanged;
    public Observable<IRemyInteractable> Transferring => _transferring;

    public void SetTarget(IRemyInteractable target)
    {
        if (_targetSetter.IsTarget(target) == false)
            return;

        _targetStateMachine.Release();
        SetCurrentTarget(target);
    }

    public void SetRandomTarget()
    {
        IRemyInteractable target = _targetStateMachine.Get();
        SetCurrentTarget(target);
    }

    public void TransferCurrentObject()
        => _transferring.OnNext(_currentTarget);

    public void Dispose()
    {
        _targetSetted?.Dispose();
        _interactableObjectChanged?.Dispose();
        _transferring?.Dispose();
    }

    private void SetCurrentTarget(IRemyInteractable target)
    {
        _targetSetted.OnNext(((MonoBehaviour)target).transform.position);
        _interactableObjectChanged.OnNext(target.GetType());

        _currentTarget = target;
    }
}
