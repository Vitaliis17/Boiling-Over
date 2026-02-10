using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    [SerializeField] private float _sensitivity;

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
        _rotater = new(_sensitivity, _maxY, _minY);
    }

    private void OnEnable()
    {
        _inputReader.MovePerformed += _mover.Move;
        _inputReader.JumpingPerformed += _jumper.Jump;
        _inputReader.InteractivePerformed += Interacte;

        _inputReader.LookPerformed += RotateX;
    }

    private void OnDisable()
    {
        _inputReader.MovePerformed -= _mover.Move;
        _inputReader.JumpingPerformed -= _jumper.Jump;
        _inputReader.InteractivePerformed -= Interacte;

        _inputReader.LookPerformed -= RotateX;
    }

    private void Interacte()
    {
        Ray ray = new(transform.position, transform.forward);
     
        if (Physics.Raycast(ray, out RaycastHit hit, 20f, 1 << (int)CustomeLayerMasks.Interactable))
            hit.transform.GetComponent<IInteractable>().Interact();
    }

    private void RotateX(Vector2 direction)
        => _rotater.RotateX(transform, direction);
}
