using UnityEngine;

[CreateAssetMenu(fileName = "SafeAudio", menuName = "ScriptableObjects/Audio/Safe")]
public class SafeAudioData : ScriptableObject
{
    [SerializeField] private AudioClip _opening;
    [SerializeField] private AudioClip _rotation;
    [SerializeField] private AudioClip _reset;

    public AudioClip Opening => _opening;
    public AudioClip Rotation => _rotation;
    public AudioClip Reset => _reset;
}
