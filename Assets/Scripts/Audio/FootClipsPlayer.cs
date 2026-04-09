using UnityEngine;

public class FootClipsPlayer : MonoBehaviour
{
    [SerializeField] private ClipsData _clipsData;
    [SerializeField] private Animator _animator;

    private float _lastValue;

    private void Update()
    {
        float currentValue = _animator.GetFloat(AnimatorParameterHashes.FootStep);

        if (Mathf.Sign(currentValue) != Mathf.Sign(_lastValue))
            AudioSource.PlayClipAtPoint(_clipsData.GetRandomClip(), transform.position, _clipsData.Volume);

        _lastValue = currentValue;
    }
}