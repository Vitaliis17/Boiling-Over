using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
using System;

public class TargetStateMachine
{
    private readonly Dictionary<TargetTypes, Pursuer> _pursuers;

    private TargetTypes _currentTargetType;

    private bool _isActive;

    public event Action<MovementActions> MovementStarted;
    public event Action Reached;
    public event Action<Vector3> Transfering;
    public event Action<TargetTypes> Interacting;

    public TargetStateMachine(NavMeshAgent agent)
    {
        _pursuers = new Dictionary<TargetTypes, Pursuer>
        {
          { TargetTypes.Player, new Pursuer(agent, MovementActions.Running) },
          { TargetTypes.Place, new Pursuer(agent, MovementActions.Walking) },
          { TargetTypes.None, new Pursuer(agent, MovementActions.Idle) }
        };

        _currentTargetType = TargetTypes.None;
    }

    public void ChangeTarget(Vector3 destination, TargetTypes targetType)
    {
        if (_pursuers[_currentTargetType].HavePath && HaveSwapPriority(targetType) == false)
            return;

        _currentTargetType = targetType;
        _pursuers[_currentTargetType].SetDestination(destination);

        if (_pursuers[_currentTargetType].HavePath)
            MovementStarted?.Invoke(_pursuers[_currentTargetType].State);
    }

    public IEnumerator CheckPathStatus()
    {
        const float WaitingTime = 0.1f;

        WaitForSeconds waiting = new(WaitingTime);

        while (_isActive)
        {
            _pursuers[_currentTargetType].UpdatePathStatus();

            if (_pursuers[_currentTargetType].HavePath == false)
            {
                Interacting?.Invoke(_currentTargetType);

                Reached?.Invoke();
                Transfering?.Invoke(_pursuers[_currentTargetType].TargetPosition);

                yield return new WaitWhile(() => _pursuers[_currentTargetType].HavePath == false);
            }

            yield return waiting;
        }
    }

    public void Activate()
        => _isActive = true;

    public void Deactivate()
        => _isActive = false;

    private bool HaveSwapPriority(TargetTypes nextType)
        => nextType <= _currentTargetType;
}