using UnityEngine;

public class Seat : MonoBehaviour, IInteractablePlace
{
    private readonly Animator _animator;
    private readonly int _animationHash;

    private readonly Transform _triggerPlace;

    public Vector3 Position => _triggerPlace.position;

    public Seat(Animator animator, int animationHash, Transform triggerPlace)
    {
        _animator = animator;
        _animationHash = animationHash;

        _triggerPlace = triggerPlace;
    }

    public void Start()
    {
        _animator.Play(_animationHash);
    }
}