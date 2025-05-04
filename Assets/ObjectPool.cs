using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    private readonly GameObject _prefab;
    private readonly Transform _parent;
    private readonly List<GameObject> _pool = new();

    public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < initialSize; i++)
            AddToPool();
    }

    private GameObject AddToPool()
    {
        var instance = GameObject.Instantiate(_prefab, _parent);
        instance.SetActive(false);
        _pool.Add(instance);
        return instance;
    }

    public GameObject Get()
    {
        var instance = _pool.FirstOrDefault(p => !p.activeSelf) ?? AddToPool();
        instance.SetActive(false);
        return instance;
    }

    public void ReturnToPool(GameObject instance)
    {
        instance.gameObject.SetActive(false);
    }
}
