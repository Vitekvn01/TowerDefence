using System;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    [SerializeField] private float _radiusAttack;
    [SerializeField] private float _timeToAttack;
    [SerializeField] private float _speed;
    
    [SerializeField] private Transform _target;

    private int _currentHealth;
    
    public float RadiusAttack => _radiusAttack;
    public float TimeToAttack => _timeToAttack;
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
        
        if (_timeToAttack < 0)
        {
            _timeToAttack = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, _radiusAttack);
    }

    public void Initialize(Transform target)
    {
        _target = target;
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnDeadEvent?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        Debug.Log("Атакую!");
        if (_target != null && _target.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.ApplyDamage(_damage);
        }
    }
    
}
