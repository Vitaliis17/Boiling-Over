using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Remy : MonoBehaviour
{
    [SerializeField] private ZoneChecker _sightZone;
    [SerializeField] private AgentAnimationData _agentAnimationData;

    [SerializeField] AgentMovement _movement;

    private AnimationStateMachine _animationStateMachine;

    private void Awake()
    {
        GetComponent<Rigidbody>().freezeRotation = true;

        Animator animator = GetComponent<Animator>();
        _animationStateMachine = new(animator, _agentAnimationData);
    }

    private void OnEnable()
        => _movement.StateChanged += _animationStateMachine.ChangeState;

    private void OnDisable()
        => _movement.StateChanged -= _animationStateMachine.ChangeState;
}