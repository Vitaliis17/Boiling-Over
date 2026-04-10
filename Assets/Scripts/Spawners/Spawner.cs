using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _container;

    private readonly ObjectPool<T> _pool;

    public Spawner(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;

        _pool = new ObjectPool<T>(Create, Get, Release, Destroy);
    }

    public T GetElement()
        => _pool.Get();

    public void ReleaseElement(T element)
        => _pool.Release(element);

    private void Release(T component)
        => component.gameObject.SetActive(false);

    private void Get(T component)
        => component.gameObject.SetActive(true);

    private void DestroyElement(T component)
        => Object.Destroy(component);

    private T Create()
        => Object.Instantiate(_prefab, _container);
}