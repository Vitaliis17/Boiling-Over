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

    private InteractablePlacesFabric _fabric;

    private void Awake()
    {
        _workingStateMachine = new();
        _animationStateMachine = new(_animator, _agentAnimationData);

        _fabric = new();
    }

    private void OnEnable()
    {
        _fabric.PlaceActivated += _workingStateMachine.SetCurrentPlace;
     
        _workingStateMachine.PlaceChanged += (Vector3 _) => _remy.DeactivateKinematic();
        _workingStateMachine.PlaceChanged += _agentMovement.SetDestination;

        _agentMovement.WalkingStarted += _animationStateMachine.ChangeState;

        _fabric.Initialize(_places);

        _agentMovement.Reached += _remy.ActivateKinematic;
        _agentMovement.Reached += _workingStateMachine.Interact;

        _workingStateMachine.StateChanged += _animationStateMachine.ChangeState;
        _workingStateMachine.RotationChanged += _remy.SetLooking;

        _fabric.PlaceDeactivated += _agentMovement.Activate;
        _fabric.PlaceDeactivated += _fabric.ActivatePlace;
    }

    private void OnDisable()
    {
        _fabric.PlaceActivated -= _workingStateMachine.SetCurrentPlace;

        _workingStateMachine.PlaceChanged -= (Vector3 _) => _remy.DeactivateKinematic();
        _workingStateMachine.PlaceChanged -= _agentMovement.SetDestination;

        _agentMovement.WalkingStarted -= _animationStateMachine.ChangeState;

        _agentMovement.Reached -= _remy.ActivateKinematic;
        _agentMovement.Reached -= _workingStateMachine.Interact;

        _workingStateMachine.StateChanged -= _animationStateMachine.ChangeState;
        _workingStateMachine.RotationChanged -= _remy.SetLooking;

        _fabric.PlaceDeactivated -= _agentMovement.Activate;
        _fabric.PlaceDeactivated -= _fabric.ActivatePlace;
    }
}