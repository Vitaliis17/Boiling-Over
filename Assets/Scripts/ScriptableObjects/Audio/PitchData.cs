using UnityEngine;

[CreateAssetMenu(fileName = "PitchData", menuName = "ScriptableObjects/Audio/Pitch")]
public class PitchData : ScriptableObject
{
    [SerializeField, Min(0)] float _minValue;
    [SerializeField, Min(0)] float _maxValue;

    public float MinValue => _minValue;
    public float MaxValue => _maxValue;

    private void OnValidate()
    {
        if (_minValue <= _maxValue)
            return;

        (_minValue, _maxValue) = (_maxValue,  _minValue);
    }
}