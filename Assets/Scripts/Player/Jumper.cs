using UnityEngine;

public class Jumper
{
    private readonly Rigidbody _rigidbody;

    private readonly float _force;

    public Jumper(float jumpingForce, Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;

        _force = jumpingForce;
    }

    public void Jump()
        => _rigidbody.AddForce(Vector3.up * _force);
}