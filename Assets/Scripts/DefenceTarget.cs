using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceTarget : MonoBehaviour
{
    [SerializeField] private int _health;
    
    private int _currentHealth;
    
    public event Action OnDeadEvent;
    private void OnValidate()
    {
        if (_health < 0)
        {
            _health = 0;
        }
    }
    
    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnDeadEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
