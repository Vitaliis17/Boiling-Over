using UnityEngine;

public class ActionSystemPresenter : MonoBehaviour
{
    [SerializeField] private InputReader[] _readers;
    [SerializeField] private InteractiveObject _interactiveObject;
    [SerializeField] private MinigameInputReader _minigameInputReader;
    [SerializeField] private SafeLock _lock;

    private InputReaderStateMachine _stateMachine;

    private void Awake()
        => _stateMachine = new(_readers);

    private void OnEnable()
    {
        _interactiveObject.Interacting += SetMinigame;
        _minigameInputReader.CancelPerformed += SetPlayer;
        _lock.Opened += SetPlayer;
    }

    private void OnDisable()
    {
        _interactiveObject.Interacting -= SetMinigame;
        _minigameInputReader.CancelPerformed -= SetPlayer;
        _lock.Opened -= SetPlayer;
    }

    private void SetPlayer()
        => _stateMachine.ChangeState(ActionMapNames.Player);

    private void SetMinigame()
        => _stateMachine.ChangeState(ActionMapNames.Minigame);
}