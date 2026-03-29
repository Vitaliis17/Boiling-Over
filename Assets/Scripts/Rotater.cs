using UnityEngine;
using DG.Tweening;

public class Rotater
{
    private readonly AnimationRotaterData _data;
    private readonly Rigidbody _rigidbody;

    public Rotater(AnimationRotaterData data, Rigidbody rigidbody)
    {
        _data = data;
        _rigidbody = rigidbody;
    }

    public void Rotate(int sign)
    {
        Vector3 endValue = _rigidbody.rotation.eulerAngles + _data.Angle * sign;

        _rigidbody.DORotate(endValue, _data.Duration);
    }
}