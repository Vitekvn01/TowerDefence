using UnityEngine;

namespace AbstractFaсtory
{
    public class TurretFactory
    {
        private readonly GameObject _prefab;

        public TurretFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public Turret Create(Vector3 position, Quaternion rotation)
        {
            GameObject go = GameObject.Instantiate(_prefab, position, rotation);
            Turret turret = go.GetComponent<Turret>();
            return turret;
        }
    }
}