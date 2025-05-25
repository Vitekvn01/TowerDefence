using System;
using UnityEngine;

public class DefenceTarget : MonoBehaviour
{
    [SerializeField] private int _health;

    public int CurrentHealth { get; private set; }
    public int Health => _health;
    public event Action<int, int> OnHealthChange; 
    public event Action OnDeadEvent;
    private void OnValidate()
    {
        if (_health < 0)
        {
            _health = 0;
        }
    }

    private void Awake()
    {
        CurrentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        
        if (CurrentHealth <= 0)
        {
            OnDeadEvent?.Invoke();
            gameObject.SetActive(false);
        }
        
        OnHealthChange?.Invoke(CurrentHealth, _health);
    }
}
