using UnityEngine;

namespace AbstractFaсtory
{
    public class TurretFactory<T> : IFactory<T> where T : Turret
    {
        public T Create(T prefab, Vector3 position, Quaternion rotation)
        {
            T instance = Object.Instantiate(prefab.gameObject, position, rotation).GetComponent<T>();
            return instance;
        }
    }
}