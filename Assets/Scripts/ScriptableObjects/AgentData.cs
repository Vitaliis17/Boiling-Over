using UnityEngine;

[CreateAssetMenu(fileName = "AgentData", menuName = "ScriptableObject/AgentData")]
public class AgentData : ScriptableObject
{
    [SerializeField, Min(0)] private float _stoppingDistance;
    [SerializeField, Min(0)] private float _speed;

    public float StoppingDistance => _stoppingDistance;
    public float Speed => _speed;
}