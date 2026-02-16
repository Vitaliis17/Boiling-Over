using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class AgentMovement : MonoBehaviour
{
    [SerializeField] private AgentData _data;

    public NavMeshAgent Agent { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();

        Agent.stoppingDistance = _data.StoppingDistance;
        Agent.speed = _data.Speed;
    }

    public void Transfer(Vector3 position)
    {
        Deactivate();
        transform.position = position;
    }

    public void Activate()
        => Agent.enabled = true;

    private void Deactivate()
        => Agent.enabled = false;
}
