using UnityEngine;

[CreateAssetMenu(fileName = "TimerData", menuName = "ScriptableObject/TimerData")]
public class TimerData : ScriptableObject
{
    [SerializeField, Min(0)] private float _minTime;
    [SerializeField, Min(0)] private float _maxTime;

    public float GenerateTime()
        => Random.Range(_minTime, _maxTime);
}