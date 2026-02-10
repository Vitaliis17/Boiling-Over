using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RotationData _rotationData;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    private Rigidbody _rigidbody;

    private Mover _mover;
    private Jumper _jumper;
    private Rotater _rotater;
    private Interacter _interacter;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mover = new(_speed, _rigidbody, transform);
        _jumper = new(_jumpingForce, _rigidbody);
        _rotater = new(_rotationData, transform);
        _interacter = new(transform);
    }

    private void OnEnable()
    {
        _inputReader.MovePerformed += _mover.Move;
        _inputReader.JumpingPerformed += _jumper.Jump;
        _inputReader.InteractivePerformed += Interacte;

        _inputReader.LookPerformed += _rotater.RotateX;
    }

    private void OnDisable()
    {
        _inputReader.MovePerformed -= _mover.Move;
        _inputReader.JumpingPerformed -= _jumper.Jump;
        _inputReader.InteractivePerformed -= Interacte;

        _inputReader.LookPerformed -= _rotater.RotateX;
    }

    private void Interacte()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, 1 << (int)CustomeLayerMasks.Interactable))
        {
            if (hit.transform.TryGetComponent(out IInteractable component))
            {
                _interacter.Interacte(component);
            }
        }
    }
}
