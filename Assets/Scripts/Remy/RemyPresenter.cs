using UnityEngine;

public class RemyPresenter : MonoBehaviour
{
    [SerializeField] private InteractablePlace[] _places;

    [SerializeField] private AgentAnimationData _agentAnimationData;

    [SerializeField] private Remy _remy;
    [SerializeField] private ZoneChecker _sight;

    [SerializeField] private AgentMovement _agentMovement;
    [SerializeField] private Pursuer _pursuer;

    [SerializeField] private Animator _animator;

    private InteractingStateMachine _interactingStateMachine;
    private AnimationStateMachine _animationStateMachine;
    private TargetStateMachine _targetStateMachine;

    private InteractablePlacesPool _placePool;

    private Coroutine _coroutine;

    private void Awake()
    {
        _interactingStateMachine = new();
        _targetStateMachine = new();
        _animationStateMachine = new(_animator, _agentAnimationData);

        _pursuer.Initialize(_agentMovement.Agent);

        _placePool = new();
    }

    private void OnEnable()
    {
        _placePool.Activated += _targetStateMachine.SetTarget;

        _targetStateMachine.TargetSetted += _remy.DeactivateKinematic;
        _targetStateMachine.TargetChanged += _pursuer.SetDestination;
        _targetStateMachine.MovementStarted += _animationStateMachine.ChangeState;

        _placePool.Initialize(_places);

        _pursuer.Reached += _remy.ActivateKinematic;
        _pursuer.Reached += _targetStateMachine.TransferCurrentObject;

        _pursuer.Transfering += _agentMovement.Transfer;

        _targetStateMachine.Transferring += _interactingStateMachine.Interact;

        _interactingStateMachine.StateChanged += _animationStateMachine.ChangeState;
        _interactingStateMachine.RotationChanged += _remy.SetLooking;

        _placePool.PlaceDeactivated += _agentMovement.Activate;
        _placePool.PlaceDeactivated += _remy.DeactivateKinematic;
        _placePool.PlaceDeactivated += _placePool.ActivatePlace;
    }

    private void OnDisable()
    {
        _placePool.Activated -= _targetStateMachine.SetTarget;

        _targetStateMachine.TargetSetted -= _remy.DeactivateKinematic;
        _targetStateMachine.TargetChanged -= _pursuer.SetDestination;
        _targetStateMachine.MovementStarted -= _animationStateMachine.ChangeState;

        _pursuer.Reached -= _remy.ActivateKinematic;
        _pursuer.Reached -= _targetStateMachine.TransferCurrentObject;

        _pursuer.Transfering -= _agentMovement.Transfer;

        _targetStateMachine.Transferring -= _interactingStateMachine.Interact;

        _interactingStateMachine.StateChanged -= _animationStateMachine.ChangeState;
        _interactingStateMachine.RotationChanged -= _remy.SetLooking;

        _placePool.PlaceDeactivated -= _agentMovement.Activate;
        _placePool.PlaceDeactivated += _remy.DeactivateKinematic;
        _placePool.PlaceDeactivated -= _placePool.ActivatePlace;
    }
}