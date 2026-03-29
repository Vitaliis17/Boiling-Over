using UnityEngine;

[CreateAssetMenu(fileName = "AnimationRotaterData", menuName = "ScriptableObject/RotaterData")]
public class AnimationRotaterData : ScriptableObject
{
    [SerializeField] private Vector3 _angle;
    [SerializeField, Min(0)] private float _duration;

    public Vector3 Angle => _angle;
    public float Duration => _duration;
}