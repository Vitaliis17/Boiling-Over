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
    private TargetStateMachine _targetStateMachine;

    private InteractablePlacesFabric _fabric;

    private Coroutine _coroutine;

    private void Awake()
    {
        _workingStateMachine = new();
        _animationStateMachine = new(_animator, _agentAnimationData);
        _targetStateMachine = new(_agentMovement.Agent);

        _fabric = new();
    }

    private void OnEnable()
    {
        _targetStateMachine.Activate();
        _coroutine = StartCoroutine(_targetStateMachine.CheckPathStatus());

        _fabric.PlaceActivated += _workingStateMachine.SetCurrentPlace;
     
        _workingStateMachine.PlaceChanged += (Vector3 _, TargetTypes _) => _remy.DeactivateKinematic();
        _workingStateMachine.PlaceChanged += _targetStateMachine.ChangeTarget;

        _targetStateMachine.MovementStarted += _animationStateMachine.ChangeState;

        _fabric.Initialize(_places);

        _targetStateMachine.Reached += _remy.ActivateKinematic;
        _targetStateMachine.Reached += _workingStateMachine.Interact;

        _targetStateMachine.Transfering += _agentMovement.Transfer;

        _workingStateMachine.StateChanged += _animationStateMachine.ChangeState;
        _workingStateMachine.RotationChanged += _remy.SetLooking;

        _fabric.PlaceDeactivated += _agentMovement.Activate;
        _fabric.PlaceDeactivated += _fabric.ActivatePlace;
    }

    private void OnDisable()
    {
        _targetStateMachine.Deactivate();
        StopCoroutine(_coroutine);

        _fabric.PlaceActivated -= _workingStateMachine.SetCurrentPlace;

        _workingStateMachine.PlaceChanged -= (Vector3 _, TargetTypes _) => _remy.DeactivateKinematic();
        _workingStateMachine.PlaceChanged -= _targetStateMachine.ChangeTarget;

        _targetStateMachine.MovementStarted -= _animationStateMachine.ChangeState;

        _targetStateMachine.Reached -= _remy.ActivateKinematic;
        _targetStateMachine.Reached -= _workingStateMachine.Interact;
      
        _targetStateMachine.Transfering -= _agentMovement.Transfer;

        _workingStateMachine.StateChanged -= _animationStateMachine.ChangeState;
        _workingStateMachine.RotationChanged -= _remy.SetLooking;

        _fabric.PlaceDeactivated -= _agentMovement.Activate;
        _fabric.PlaceDeactivated -= _fabric.ActivatePlace;
    }
}