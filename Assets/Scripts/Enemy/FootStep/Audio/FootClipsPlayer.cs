using UnityEngine;
using System;

public class FootClipsPlayer : MonoBehaviour
{
    [SerializeField] private ClipsData _clipsData;
    [SerializeField] private Animator _animator;

    private float _lastValue;

    public event Action<AudioClip, Vector3, float> Stepped;

    private void Update()
    {
        float currentValue = _animator.GetFloat(AnimatorParameterHashes.FootStep);

        if (Mathf.Sign(currentValue) != Mathf.Sign(_lastValue))
            Stepped?.Invoke(_clipsData.GetRandomClip(), transform.position, _clipsData.Volume);

        _lastValue = currentValue;
    }
}