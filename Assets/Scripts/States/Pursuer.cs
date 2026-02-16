using UnityEngine;
using UnityEngine.AI;

public class Pursuer
{
    private readonly NavMeshAgent _agent;

    public Pursuer(NavMeshAgent agent, MovementActions state)
    {
        _agent = agent;
        State = state;

        HavePath = true;
    }

    public MovementActions State { get; }

    public Vector3 TargetPosition { get; private set; }
    public bool HavePath { get; private set; }

    public void SetDestination(Vector3 destination)
    {
        TargetPosition = destination;
        HavePath = _agent.SetDestination(TargetPosition);
    }

    public void UpdatePathStatus()
    {
        if (HavePath == false)
            return;

        HavePath = _agent.pathPending == false && _agent.remainingDistance > _agent.stoppingDistance;
    }
}