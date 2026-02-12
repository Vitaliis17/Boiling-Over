using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMovement : MonoBehaviour
{
    [SerializeField] private AgentData _data;

    private NavMeshAgent _agent;

    private bool _havePath;

    private Coroutine _coroutine;

    public event Action<States> StateChanged;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.stoppingDistance = _data.StoppingDistance;
        _agent.speed = _data.Speed;
    }

    private void OnEnable()
        => _coroutine = StartCoroutine(CheckWalking());

    private void OnDisable()
        => StopCoroutine(_coroutine);

    public void SetDestination(Vector3 position)
    {
        _havePath = _agent.SetDestination(position);

        if (_havePath)
            StateChanged?.Invoke(States.Walking);
    }

    private IEnumerator CheckWalking()
    {
        const float WaitTime = 0.1f;

        WaitForSeconds waitingTime = new(WaitTime);

        while (enabled)
        {
            if(_havePath && _agent.isStopped)
            {
                _havePath = false;
                StateChanged?.Invoke(States.Idle);
            }

            yield return waitingTime;
        }
    }
}
