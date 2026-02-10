using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    private Rigidbody _rigidbody;

    private Mover _mover;
    private Jumper _jumper;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 2);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;

        _mover = new(_speed, _rigidbody, transform);
        _jumper = new(_jumpingForce, _rigidbody);
    }

    private void OnEnable()
    {
        _inputReader.MovePerformed += _mover.Move;
        _inputReader.JumpingPerformed += _jumper.Jump;
        _inputReader.InteractivePerformed += Interacte;
    }

    private void OnDisable()
    {
        _inputReader.MovePerformed -= _mover.Move;
        _inputReader.JumpingPerformed -= _jumper.Jump;
        _inputReader.InteractivePerformed -= Interacte;
    }

    private void Interacte()
    {
        Ray ray = new(transform.position, transform.forward);
     
        if (Physics.Raycast(ray, out RaycastHit hit, 20f, 1 << (int)CustomeLayerMasks.Interactable))
            hit.transform.GetComponent<IInteractable>().Interact();
    }
}
