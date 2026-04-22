using UnityEngine;
using R3;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IRemyInteractable
{
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private LookRotationData _rotationData;

    [SerializeField] private Interacter _interacter;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    private Mover _mover;
    private Jumper _jumper;
    private LookRotater _rotater;

    private DisposableBag _bag = new();

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;

        _mover = new(_speed, rigidbody, transform);
        _jumper = new(_jumpingForce, rigidbody);
        _rotater = new(_rotationData, transform);
    }

    private void OnEnable()
    {
        _inputReader.Moved.Subscribe(direction => _mover.Move(direction)).AddTo(ref _bag);
        _inputReader.Looked.Subscribe(direction => _rotater.RotateX(direction)).AddTo(ref _bag);

        _inputReader.Jumped.Subscribe(_ => _jumper.Jump()).AddTo(ref _bag);
        _inputReader.Interacted.Subscribe(_ => _interacter.Interact()).AddTo(ref _bag);
    }

    private void OnDisable()
        => _bag.Clear();

    private void OnDestroy()
        => _bag.Dispose();

    public void Interact()
        => Destroy(gameObject);
}