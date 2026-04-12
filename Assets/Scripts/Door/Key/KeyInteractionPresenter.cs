using UnityEngine;

public class KeyInteractionPresenter : MonoBehaviour
{
    [SerializeField] private Key _key;
    [SerializeField] private DoorLock[] _locks;

    private void OnEnable()
        => Subscribe();

    private void OnDisable()
        => Unsubscribe();

    private void Subscribe()
    {
        foreach(DoorLock doorLock in _locks)
            _key.Using += doorLock.TryUnlock;
    }

    private void Unsubscribe()
    {
        foreach(DoorLock doorLock in _locks)
            _key.Using -= doorLock.TryUnlock;
    }
}