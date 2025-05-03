using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    
    [SerializeField] private Transform _tower;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private float _timeToFire;
    [SerializeField] private float _radiusFire;
    [SerializeField] private float _rotationSpeed;
    
    [SerializeField] private int _damage;
    
    private Transform _target;

    private float _timer;
    
    public float RadiusFire => _radiusFire;
    
    public Transform Target => _target;

    
    private void Update()
    {
        if (_target != null)
        {
            RotateToTarget();
            
            _timer += Time.deltaTime;
            if (_timer >= _timeToFire)
            {
                Fire();
                _timer = 0f;
            }
        }
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private void RotateToTarget()
    {
        Vector3 direction = _target.position - _tower.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _tower.rotation = Quaternion.Lerp(_tower.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    private void Fire()
    {
        Bullet bullet = _bulletSpawner.SpawnBullet(_shootPoint.transform.position, _shootPoint.rotation);
        bullet.Initialization(_damage);
    }
    
}
