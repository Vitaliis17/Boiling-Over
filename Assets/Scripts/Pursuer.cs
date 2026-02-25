using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Pursuer : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Coroutine _coroutine;

    private Vector3 _targetPosition;
    private bool _havePath;

    public event Action Reached;
    public event Action<Vector3> Transfering;

    private void Awake()
        => _havePath = false;

    private void OnDisable()
        => StopCoroutine(_coroutine);

    public void Initialize(NavMeshAgent agent)
    {
        _agent = agent;

        _coroutine = StartCoroutine(CheckPathStatus());
    }

    public void SetDestination(Vector3 destination)
    {
        _targetPosition = destination;
        _havePath = _agent.SetDestination(_targetPosition);
    }

    private void UpdatePathStatus()
        => _havePath = _agent.pathPending == false && _agent.remainingDistance > _agent.stoppingDistance;

    private IEnumerator CheckPathStatus()
    {
        const float WaitingTime = 0.1f;

        WaitForSeconds waiting = new(WaitingTime);

        while (enabled)
        {
            UpdatePathStatus();

            if (_havePath == false)
            {
                Reached?.Invoke();
                Transfering?.Invoke(_targetPosition);

                yield return new WaitWhile(() => _havePath == false);
            }

            yield return waiting;
        }
    }
}