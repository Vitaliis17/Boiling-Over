using UnityEngine;

public class Rotater
{
    private readonly float _sensitivity;

    private readonly float _maxY;
    private readonly float _minY;

    private float _rotationX;

    public Rotater(float sensitivity, float maxY, float minY)
    {
        _sensitivity = sensitivity;

        _maxY = maxY;
        _minY = minY;

        _rotationX = 0;
    }

    public void RotateY(Transform transform, Vector2 rotation)
    {
        _rotationX -= rotation.y * _sensitivity * Time.fixedDeltaTime;
        _rotationX = Mathf.Clamp(_rotationX, _minY, _maxY);

        transform.localRotation = Quaternion.Euler(_rotationX, transform.rotation.y, transform.rotation.z);
    }

    public void RotateX(Transform transform, Vector2 rotation)
        => transform.Rotate(Vector3.up * rotation.x * _sensitivity * Time.fixedDeltaTime);
}