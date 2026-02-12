using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Remy : MonoBehaviour
{
    [SerializeField] private ZoneChecker _sightZone;

    private NavMeshAgent _agent;

    private void Awake()
     => _agent = GetComponent<NavMeshAgent>();

    private void OnEnable()
        => _sightZone.PlayerFinded += SetDestination;

    private void OnDisable()
        => _sightZone.PlayerFinded -= SetDestination;

    private void SetDestination(Player player)
        => _agent.destination = player.transform.position;
}
