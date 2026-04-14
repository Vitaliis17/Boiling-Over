using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private KeyData _requiredKey;

    public void TryUnlock(Key key)
    {
        if (key.IsEqualName(_requiredKey.Name) == false)
            return;

        Unlock();
    }

    private void Unlock()
        => gameObject.SetActive(false);
}