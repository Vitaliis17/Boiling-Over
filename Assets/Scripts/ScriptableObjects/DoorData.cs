using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "ScriptableObject/DoorData")]
public class DoorData : ScriptableObject
{
    [SerializeField] private float _closeRotatation;
    [SerializeField] private float _openRotation;

    [SerializeField, Min(0)] private float _animationDuration;

    public Vector3 CloseRotation { get; private set; }
    public Vector3 OpenRotation { get; private set; }

    public float AnimationDuration => _animationDuration;

    private void OnValidate()
    {
        CloseRotation = Vector3.up * _closeRotatation;
        OpenRotation = Vector3.up * _openRotation;
    }
}