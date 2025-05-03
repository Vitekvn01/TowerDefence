using UnityEngine;

public class BulletSpawner : ObjectPool
{
    [SerializeField] private Bullet _bulletPrefab;
    private void Start()
    {
        Initialize(_bulletPrefab.gameObject);
    }

    public Bullet SpawnBullet(Vector3 pos, Quaternion rot)
    {
        GetObject(out GameObject spawned);

        Bullet bullet = spawned.GetComponent<Bullet>();

        bullet.transform.position = pos;
        bullet.transform.rotation = rot;
        
        bullet.gameObject.SetActive(true);
        
        return bullet;
    }
}
