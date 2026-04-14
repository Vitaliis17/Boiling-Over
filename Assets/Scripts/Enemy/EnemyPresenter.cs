using UnityEngine;

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
    private AnimationStrategySelector _animationStateMachine;
    private TargetStateMachine _targetStateMachine;

    private InteractablePlacesPool _placePool;

    private void Awake()
    {
        _timeInteractionMachine = new();
        _interactingStateMachine = new();
        _targetStateMachine = new();
        _animationStateMachine = new(_animator, _agentAnimationData);

        _pursuer.Initialize(_agentMovement.Agent);

        _placePool = new();
        _placePool.Initialize(_places);
    }

    private void OnEnable()
    {
        _targetStateMachine.Releasing += _placePool.Release;

        _targetStateMachine.CurrentObjectChanging += _placePool.ActivatePlace;

        _sight.PlayerFinded += _targetStateMachine.SetTarget;
        _sight.PlayerEscaped += _targetStateMachine.GetInteractableObject;

        _targetStateMachine.TargetSetted += _enemy.DeactivateKinematic;
        _targetStateMachine.TargetSetted += _timeInteractionMachine.StopInteraction;

        _targetStateMachine.TargetChanged += _pursuer.SetDestination;
        _targetStateMachine.MovementStarted += _animationStateMachine.ChangeState;

        _pursuer.Reached += _enemy.ActivateKinematic;
        _pursuer.Reached += _targetStateMachine.TransferCurrentObject;

        _pursuer.Transfering += _agentMovement.Transfer;

        _targetStateMachine.Transferring += _interactingStateMachine.Interact;
        _targetStateMachine.Transferring += _timeInteractionMachine.Interact;

        _interactingStateMachine.StateChanged += _animationStateMachine.ChangeState;
        _interactingStateMachine.RotationChanged += _enemy.SetLooking;

        _timeInteractionMachine.InteractionOvered += _agentMovement.Activate;
        _timeInteractionMachine.InteractionOvered += _enemy.DeactivateKinematic;

        _timeInteractionMachine.InteractionCompleted += _targetStateMachine.GetInteractableObject;

        _targetStateMachine.GetInteractableObject();
    }

    private void OnDisable()
    {
        _targetStateMachine.Releasing -= _placePool.Release;

        _targetStateMachine.CurrentObjectChanging -= _placePool.ActivatePlace;

        _sight.PlayerFinded -= _targetStateMachine.SetTarget;
        _sight.PlayerEscaped -= _targetStateMachine.GetInteractableObject;

        _targetStateMachine.TargetSetted -= _enemy.DeactivateKinematic;
        _targetStateMachine.TargetSetted -= _timeInteractionMachine.StopInteraction;

        _targetStateMachine.TargetChanged -= _pursuer.SetDestination;
        _targetStateMachine.MovementStarted -= _animationStateMachine.ChangeState;

        _pursuer.Reached -= _enemy.ActivateKinematic;
        _pursuer.Reached -= _targetStateMachine.TransferCurrentObject;

        _pursuer.Transfering -= _agentMovement.Transfer;

        _targetStateMachine.Transferring -= _interactingStateMachine.Interact;
        _targetStateMachine.Transferring -= _timeInteractionMachine.Interact;

        _interactingStateMachine.StateChanged -= _animationStateMachine.ChangeState;
        _interactingStateMachine.RotationChanged -= _enemy.SetLooking;

        _timeInteractionMachine.InteractionOvered -= _agentMovement.Activate;
        _timeInteractionMachine.InteractionOvered -= _enemy.DeactivateKinematic;

        _timeInteractionMachine.InteractionCompleted -= _targetStateMachine.GetInteractableObject;
    }
}