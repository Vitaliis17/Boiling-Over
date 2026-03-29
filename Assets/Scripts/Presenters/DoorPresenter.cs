using UnityEngine;

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
        _interactiveObject.Interacting += _doorStateMachine.ChangeState;

        _doorStateMachine.Opened += _door.Open;
        _doorStateMachine.Closed += _door.Close;
    }

    private void OnDisable()
    {
        _interactiveObject.Interacting -= _doorStateMachine.ChangeState;

        _doorStateMachine.Opened -= _door.Open;
        _doorStateMachine.Closed -= _door.Close;
    }
}