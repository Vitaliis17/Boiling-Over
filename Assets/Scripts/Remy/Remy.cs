using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Remy : MonoBehaviour
{
    [SerializeField] private ZoneChecker _sight;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    public void ActivateKinematic()
        => _rigidbody.isKinematic = true;

    public void DeactivateKinematic()
        => _rigidbody.isKinematic = false;

    public void SetLooking(Quaternion rotation)
        => transform.rotation = rotation;
}