using UnityEngine;
using DG.Tweening;

public class Door
{
    private readonly DoorData _data;
    private readonly Transform _transform;

    public Door(Transform transform, DoorData data)
    {
        _transform = transform;
        _data = data;
    }

    public void Open()
        => Rotate(_data.OpenRotation);

    public void Close()
        => Rotate(_data.CloseRotation);

    private void Rotate(Vector3 rotation)
        => _transform.DOLocalRotate(rotation, _data.AnimationDuration);
}