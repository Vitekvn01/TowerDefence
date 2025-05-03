using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    
    [SerializeField] private float _speed;
    
    [SerializeField] private Transform _target;
    
    public float Speed => _speed;
    public Transform Target => _target;
    
    public event Action<Enemy> OnDeadEvent;
    private void OnValidate()
    {
        if (_health < 0)
        {
            _health = 0;
        }

        if (_speed < 0)
        {
            _speed = 0;
        }
    }

    public void Initialize(Transform target)
    {
        _target = target;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            OnDeadEvent?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
    
}
