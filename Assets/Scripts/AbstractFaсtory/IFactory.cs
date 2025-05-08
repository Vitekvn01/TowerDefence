using UnityEngine;

namespace AbstractFaсtory
{
    public interface IFactory<T>
    {
        T Create(Vector3 position, Quaternion rotation);
    }
}