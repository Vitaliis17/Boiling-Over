using UnityEngine;
using R3;

public class DoorPresenter : MonoBehaviour
{
    [SerializeField] private DoorData _data;
    [SerializeField] private InteractiveObject _interactiveObject;

    private Door _door;
    private DoorStateMachine _doorStateMachine;

    private void Awake()
    {
        _door = new(transform, _data);
        _doorStateMachine = new();
    }

    private void OnEnable()
    {
        _interactiveObject.Interacting.Subscribe(_ => _doorStateMachine.ChangeState()).AddTo(this);

        _doorStateMachine.Opened.Subscribe(_ => _door.Open()).AddTo(this);
        _doorStateMachine.Closed.Subscribe(_ => _door.Close()).AddTo(this);
    }

    private void OnDestroy()
        => _doorStateMachine?.Dispose();
}