using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RotationData _rotationData;

    [SerializeField] private Interacter _interacter;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    private Rigidbody _rigidbody;

    private Mover _mover;
    private Jumper _jumper;
    private Rotater _rotater;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mover = new(_speed, _rigidbody, transform);
        _jumper = new(_jumpingForce, _rigidbody);
        _rotater = new(_rotationData, transform);
    }

    private void OnEnable()
    {
        _inputReader.MovePerformed += _mover.Move;
        _inputReader.JumpingPerformed += _jumper.Jump;
        _inputReader.InteractivePerformed += _interacter.Interacte;

        _inputReader.LookPerformed += _rotater.RotateX;
    }

    private void OnDisable()
    {
        _inputReader.MovePerformed -= _mover.Move;
        _inputReader.JumpingPerformed -= _jumper.Jump;
        _inputReader.InteractivePerformed -= _interacter.Interacte;

        _inputReader.LookPerformed -= _rotater.RotateX;
    }
}
