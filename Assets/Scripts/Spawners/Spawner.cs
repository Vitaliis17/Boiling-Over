using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> where T : Component
{
    private ObjectPool<T> _pool;
    private T _prefab;

    public Spawner()
        => _pool = new ObjectPool<T>(Create, Get, Release, Destroy);

    public T GetElement()
        => _pool.Get();

    public void ReleaseElement(T element)
        => _pool.Release(element);

    private void Release(T component)
        => component.gameObject.SetActive(false);

    private void Get(T component)
        => component.gameObject.SetActive(true);

    private void Destroy(T component)
        => Destroy(component);

    private T Create()
        => Object.Instantiate(_prefab);
}