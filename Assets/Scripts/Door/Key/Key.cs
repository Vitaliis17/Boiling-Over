using UnityEngine;
using R3;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyData _data;

    private readonly Subject<Key> _using = new();

    public Observable<Key> Using => _using;

    private void OnDestroy()
        => _using?.Dispose();

    public void Interact()
    {
        _using.OnNext(this);

        gameObject.SetActive(false);
    }

    public bool IsEqualName(string name)
        => _data.Name == name;
}