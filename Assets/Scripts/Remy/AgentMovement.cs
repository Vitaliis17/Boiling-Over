using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class AgentMovement : MonoBehaviour
{
    [SerializeField] private AgentData _data;

    private NavMeshAgent _agent;

    private bool _havePath;

    private Coroutine _coroutine;
    private Vector3 _target;

    public event Action<States> WalkingStarted;
    public event Action Reached;

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
        {
            WalkingStarted?.Invoke(States.Walking);
            _target = position;
        }
    }

    public void Activate()
        => _agent.enabled = true;

    private void Deactivate()
        => _agent.enabled = false;

    private IEnumerator CheckWalking()
    {
        const float WaitTime = 0.1f;

        WaitForSeconds waitingTime = new(WaitTime);

        while (enabled)
        {
            if (_havePath && _agent.pathPending == false && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                Deactivate();

                _havePath = false;
                transform.position = _target;
                Reached?.Invoke();
            }

            yield return waitingTime;
        }
    }
}
