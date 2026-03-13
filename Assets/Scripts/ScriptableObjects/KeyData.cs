using UnityEngine;

[CreateAssetMenu(fileName = "KeyData", menuName = "ScriptableObject/KeyData")]
public class KeyData : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name => _name;
}