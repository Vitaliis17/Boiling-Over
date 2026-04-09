using UnityEngine;

[CreateAssetMenu(fileName = "ClipsData", menuName = "ScriptableObjects/Audio/ClipsData")]
public class ClipsData : ScriptableObject
{
    [SerializeField] AudioClip[] _clips;
    [SerializeField, Range(0, 1)] float _volume;

    public float Volume => _volume;

    public AudioClip GetRandomClip()
    {
        int randomIndex = Random.Range(0, _clips.Length);

        return _clips[randomIndex];
    }
}