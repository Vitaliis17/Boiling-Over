using UnityEngine;
using R3;

public class ActionSystemPresenter : MonoBehaviour
{
    [SerializeField] private InputReader[] _readers;
    [SerializeField] private InteractiveObject _interactiveObject;
    [SerializeField] private MinigameInputReader _minigameInputReader;
    [SerializeField] private SafeLock _lock;

    private InputReaderStateMachine _stateMachine;

    private void Awake()
        => _stateMachine = new(_readers);

    private void Start()
    {
        _lock.Opened.Subscribe(_ => SetPlayer()).AddTo(this);
        _interactiveObject.Interacting.Subscribe(_ => SetMinigame()).AddTo(this); ;
        _minigameInputReader.Cancelled.Subscribe(_ => SetPlayer()).AddTo(this);
    }

    private void SetPlayer()
        => _stateMachine.ChangeState(ActionMapNames.Player);

    private void SetMinigame()
        => _stateMachine.ChangeState(ActionMapNames.Minigame);
}