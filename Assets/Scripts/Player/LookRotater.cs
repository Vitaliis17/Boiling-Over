using UnityEngine;

public class LookRotater
{
    private readonly LookRotationData _rotationData;
    private readonly Transform _transform;

    private float _rotationX;

    public LookRotater(LookRotationData rotationData, Transform transform)
    {
        _rotationData = rotationData;
        _transform = transform;

        _rotationX = 0;
    }

    public void RotateY(Vector2 rotation)
    {
        _rotationX -= rotation.y * _rotationData.SensitivityY * Time.fixedDeltaTime;
        _rotationX = Mathf.Clamp(_rotationX, _rotationData.MinY, _rotationData.MaxY);

        _transform.localRotation = Quaternion.Euler(_rotationX, _transform.rotation.y, _transform.rotation.z);
    }

    public void RotateX(Vector2 rotation)
        => _transform.Rotate(Vector3.up * rotation.x * _rotationData.SensitivityX * Time.fixedDeltaTime);
}