using UnityEngine;
using R3;

public class FootClipsPlayer : MonoBehaviour
{
    [SerializeField] private ClipsData _clipsData;
    [SerializeField] private Animator _animator;

    private readonly Subject<(AudioClip, Vector3, float)> _stepped = new();

    private float _lastValue;

    public Observable<(AudioClip, Vector3, float)> Stepped => _stepped;

    private void Update()
    {
        float currentValue = _animator.GetFloat(AnimatorParameterHashes.FootStep);

        if (Mathf.Sign(currentValue) != Mathf.Sign(_lastValue))
            _stepped.OnNext((_clipsData.GetRandomClip(), transform.position, _clipsData.Volume));

        _lastValue = currentValue;
    }

    private void OnDestroy()
        => _stepped?.Dispose();
}