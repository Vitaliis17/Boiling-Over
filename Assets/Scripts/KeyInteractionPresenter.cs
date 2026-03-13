using UnityEngine;

public class KeyInteractionPresenter : MonoBehaviour
{
    [SerializeField] private Key _key;
    [SerializeField] private Lock[] _locks;

    private void OnEnable()
        => Subscribe();

    private void OnDisable()
        => Unsubscribe();

    private void Subscribe()
    {
        foreach(Lock doorLock in _locks)
            _key.Using += doorLock.TryUnlock;
    }

    private void Unsubscribe()
    {
        foreach(Lock doorLock in _locks)
            _key.Using -= doorLock.TryUnlock;
    }
}