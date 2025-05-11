using AbstractFaсtory;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    private IFactory<Turret> _factory;

    public void Initialize(IFactory<Turret> factory)
    {
        _factory = factory;
    }

    public void SpawnTurret(Turret prefab, Vector3 position)
    {
        _factory.Create(prefab, position, Quaternion.identity);
    }
}
