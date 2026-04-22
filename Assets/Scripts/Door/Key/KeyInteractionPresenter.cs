using UnityEngine;
using R3;

public class KeyInteractionPresenter : MonoBehaviour
{
    [SerializeField] private Key _key;
    [SerializeField] private DoorLock[] _locks;

    private void Start()
        => Subscribe();

    private void Subscribe()
    {
        foreach (DoorLock doorLock in _locks)
            _key.Using.Subscribe(key => doorLock.TryUnlock(key)).AddTo(this);
    }
}