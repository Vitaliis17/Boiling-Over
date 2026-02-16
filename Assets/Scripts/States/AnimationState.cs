using UnityEngine;

public class AnimationState
{
    private readonly Animator _animator;
    private readonly int _animationHash;

    public AnimationState(Animator animator, int animationHash)
    {
        _animator = animator;
        _animationHash = animationHash;
    }

    public void Start(int layer)
        => _animator.Play(_animationHash, layer);
}