using UnityEngine;

public class SafeAudioPlayer : MonoBehaviour
{
    [SerializeField] private SafeAudioData _safeData;
    [SerializeField] private PitchData _pitchData;

    [SerializeField] private AudioSource _source;

    public void PlayRotation()
    {
        GeneratePitch();
        _source.PlayOneShot(_safeData.Rotation);
    }

    public void PlayOpen()
    {
        GeneratePitch();
        _source.PlayOneShot(_safeData.Opening);
    }

    public void PlayReset()
    {
        GeneratePitch();
        _source.PlayOneShot(_safeData.Reset);
    }

    private void GeneratePitch()
        => _source.pitch = Random.Range(_pitchData.MinValue, _pitchData.MinValue);
}