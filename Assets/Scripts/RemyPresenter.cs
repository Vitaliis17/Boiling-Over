using UnityEngine;

public class RemyPresenter : MonoBehaviour
{
    [SerializeField] private InteractablePlace[] _places;
 
    [SerializeField] private AgentAnimationData _agentAnimationData;

    [SerializeField] private Remy _remy;
    [SerializeField] private AgentMovement _agentMovement;

    [SerializeField] private Animator _animator;

    private WorkingStateMachine _workingStateMachine;
    private AnimationStateMachine _animationStateMachine;

    private void Awake()
    {
        _workingStateMachine = new();
        _animationStateMachine = new(_animator, _agentAnimationData);
    }

    private void OnEnable()
    {
        _workingStateMachine.PlaceChanged += _agentMovement.SetDestination;
        _agentMovement.StateChanged += _animationStateMachine.ChangeState;

        _workingStateMachine.Initialize(_places);

        _agentMovement.Reached += _workingStateMachine.CurrentPlace.Interact;
        _workingStateMachine.CurrentPlace.StartInteracting += _animationStateMachine.ChangeState;
        _workingStateMachine.CurrentPlace.ContinueInteracting += _agentMovement.SetLooking;
    }

    private void OnDisable()
    {
        _workingStateMachine.PlaceChanged -= _agentMovement.SetDestination;
        _agentMovement.StateChanged -= _animationStateMachine.ChangeState;

        _agentMovement.Reached -= _workingStateMachine.CurrentPlace.Interact;
        _workingStateMachine.CurrentPlace.StartInteracting -= _animationStateMachine.ChangeState;
        _workingStateMachine.CurrentPlace.ContinueInteracting -= _agentMovement.SetLooking;
    }
}