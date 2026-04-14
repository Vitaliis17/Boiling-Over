using UnityEngine;

public class Mover : MonoBehaviour
{
    private readonly float _speed;
    
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;

    public Mover(float speed, Rigidbody rigidbody, Transform transform)
    {
        _speed = speed;

        _rigidbody = rigidbody;
        _transform = transform;
    }

    public void Move(Vector2 direction)
    {
        Vector3 delta = (_transform.forward * direction.y + _transform.right * direction.x) * Time.fixedDeltaTime * _speed;

        _rigidbody.MovePosition(_rigidbody.position + delta);
    }
}