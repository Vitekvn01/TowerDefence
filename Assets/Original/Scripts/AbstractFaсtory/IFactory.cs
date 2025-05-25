using UnityEngine;

namespace AbstractFaсtory
{
    public interface IFactory<T>
    {
        T Create(T prefab, Vector3 position, Quaternion rotation);
    }
}