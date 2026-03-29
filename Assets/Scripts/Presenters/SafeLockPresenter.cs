using UnityEngine;

public class SafeLockPresenter : MonoBehaviour
{
    [SerializeField] private SafeLock _lock;
    [SerializeField] private AnimationRotaterData _rotaterData;
    [SerializeField] private MinigameInputReader _inputReader;

    [SerializeField] private Rigidbody _rigidbody;

    private Rotater _rotater;

    private void Awake()
        => _rotater = new(_rotaterData, _rigidbody);

    private void OnEnable()
    {
        _inputReader.EnterePerformed += _lock.Enter;
        _inputReader.TurnPerformed += _lock.Turn;
        _lock.Changed += _rotater.Rotate;
    }

    private void OnDisable()
    {
        _inputReader.EnterePerformed -= _lock.Enter;
        _inputReader.TurnPerformed -= _lock.Turn;
        _lock.Changed -= _rotater.Rotate;
    }
}