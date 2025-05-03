using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private int _initialSize = 20;
    
    private GameObject _prefab;
    
    private List<GameObject> _pool = new List<GameObject>();
    
    protected void Initialize(GameObject prefab)
    {
        _prefab = prefab;
        for (int i = 0; i < _initialSize; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject(int count = 1)
    {
        GameObject spawned = Instantiate(_prefab, _parent.transform);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }
    
    protected void GetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => !p.activeSelf);

        if (result == null)
        {
            result = CreateObject(_initialSize);
        }

    }
}
