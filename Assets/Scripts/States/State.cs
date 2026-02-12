using UnityEngine;

public class State
{
    private readonly Animator _animator;
    private readonly int _animationHash;

    public State(Animator animator, int animationHash)
    {
        _animator = animator;
        _animationHash = animationHash;
    }

    public void Start()
        => _animator.Play(_animationHash);
}