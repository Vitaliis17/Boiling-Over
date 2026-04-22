using UnityEngine;
using R3;

public class EnemyPresenter : MonoBehaviour
{
    [SerializeField] private InteractablePlace[] _places;

    [SerializeField] private AgentAnimationData _agentAnimationData;

    [SerializeField] private Enemy _enemy;
    [SerializeField] private ZoneChecker _sight;

    [SerializeField] private AgentMovement _agentMovement;
    [SerializeField] private Pursuer _pursuer;

    [SerializeField] private Animator _animator;

    private TimeInteractionStateMachine _timeInteractionMachine;
    private InteractingStateMachine _interactingStateMachine;
    private AnimationSelector _animationStateMachine;
    private MovementActionSetter _movementActionSetter;

    private TargetCoordinator _targetCoordinator;

    private void Awake()
    {
        _timeInteractionMachine = new();
        _interactingStateMachine = new();

        _animationStateMachine = new(_animator, _agentAnimationData);
        _movementActionSetter = new();

        InteractablePlacesPool placePool = new();
        TargetPool targetStateMachine = new(placePool, _places);
        
        PriorityTargetsContainer targetSetter = new();

        _targetCoordinator = new(targetStateMachine, targetSetter);

        _pursuer.Initialize(_agentMovement.Agent);
    }

    private void Start()
    {
        _sight.PlayerFinded.Subscribe(player => _targetCoordinator.SetTarget(player)).AddTo(this);
        _sight.PlayerEscaped.Subscribe(_ => _targetCoordinator.SetRandomTarget()).AddTo(this);

        _targetCoordinator.TargetSetted.Subscribe(targetPosition =>
        {
            _enemy.DeactivateKinematic();
            _timeInteractionMachine.StopInteraction();

            _pursuer.SetDestination(targetPosition);
        }).AddTo(this);

        _targetCoordinator.InteractableObjectChanged.Subscribe(type => _movementActionSetter.Set(type)).AddTo(this);
        _movementActionSetter.Setted.Subscribe(action => _animationStateMachine.ChangeState(action)).AddTo(this);

        _pursuer.Reached.Subscribe(_ =>
        {
            _enemy.ActivateKinematic();
            _targetCoordinator.TransferCurrentObject();
        }).AddTo(this);

        _pursuer.Transfering.Subscribe(position => _agentMovement.Transfer(position)).AddTo(this);

        _targetCoordinator.Transferring.Subscribe(target =>
        {
            _interactingStateMachine.Interact(target);
            _timeInteractionMachine.Interact(target);
        }).AddTo(this);
        
        _interactingStateMachine.StateChanged.Subscribe(action => _animationStateMachine.ChangeState(action)).AddTo(this);
        _interactingStateMachine.RotationChanged.Subscribe(rotation => _enemy.SetLooking(rotation)).AddTo(this);;

        _timeInteractionMachine.InteractionOvered.Subscribe(_ =>
        {
            _agentMovement.Activate();
            _enemy.DeactivateKinematic();
        }).AddTo(this);

        _timeInteractionMachine.InteractionCompleted.Subscribe(_ => _targetCoordinator.SetRandomTarget()).AddTo(this);

        _targetCoordinator.SetRandomTarget();
    }

    private void OnDestroy()
    {
        _timeInteractionMachine?.Dispose();
        _interactingStateMachine?.Dispose();
        _movementActionSetter?.Dispose();
        _targetCoordinator?.Dispose();
    }
}