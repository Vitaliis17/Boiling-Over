using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootStep : MonoBehaviour
{
    private AudioSource _source;

    private void Awake()
        => _source = GetComponent<AudioSource>();

    public void Play(AudioClip clip, float volume)
    {
        _source.clip = clip;
        _source.volume = volume;

        _source.Play();
    }
}