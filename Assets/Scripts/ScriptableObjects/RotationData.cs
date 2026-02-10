using UnityEngine;

[CreateAssetMenu(fileName = "RotationData", menuName = "ScriptableObject/RotationData")]
public class RotationData : ScriptableObject
{
    [SerializeField, Min(0)] private float _sensitivityX;
    [SerializeField, Min(0)] private float _sensitivityY;

    [SerializeField] private float _maxY;
    [SerializeField] private float _minY;

    public float SensitivityX => _sensitivityX;
    public float SensitivityY => _sensitivityY;
    public float MaxY => _maxY;
    public float MinY => _minY;
}