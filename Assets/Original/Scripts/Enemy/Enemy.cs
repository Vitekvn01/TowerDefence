using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string RunAnimation = "Run";
    private const string AttackAnimation = "Attack";
    private const string DamageAnimation = "Damage";
    private const string WalkAnimation = "Walk";
    
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    
    [SerializeField] private float _radiusAttack;
    [SerializeField] private float _timeToAttack;
    [SerializeField] private float _speed;
    
    [SerializeField] private Animator _animator;
    
    private DefenceTarget _target;

    private int _currentHealth;

    public int Reward => _reward;
    public float RadiusAttack => _radiusAttack;
    public float TimeToAttack => _timeToAttack;
    public float Speed => _speed;
    public Transform Target => _target.transform;
    
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

        if (_reward < 0)
        {
            _reward = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, _radiusAttack);
    }

    public void Initialize(DefenceTarget target)
    {
        _target = target;
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        _animator.Play(DamageAnimation);
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnDeadEvent?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        _animator.Play(AttackAnimation);
        Debug.Log("Атакую!");
        if (_target != null)
        {   
            _target.ApplyDamage(_damage);
        }
    }
    
}
