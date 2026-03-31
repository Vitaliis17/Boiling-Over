using UnityEngine;

[CreateAssetMenu(fileName = "KeyData", menuName = "ScriptableObjects/KeyData")]
public class KeyData : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name => _name;
}