using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    
    [SerializeField] private Transform _tower;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private float _timeToFire;
    [SerializeField] private float _radiusFire;
    [SerializeField] private float _rotationSpeed;
    
    [SerializeField] private int _damage;
    
    private Transform _target;

    private float _timer;

    private ObjectPool _bulletPool;
    
    public float RadiusFire => _radiusFire;
    
    public Transform Target => _target;

    private void Awake()
    {
        _bulletPool = new ObjectPool(_bulletPrefab.gameObject, 100, gameObject.transform);
    }

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
        Bullet bullet = _bulletPool.Get().GetComponent<Bullet>();
        
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
        
        bullet.gameObject.SetActive(true);;
        bullet.Initialization(_damage);
    }
    
}
