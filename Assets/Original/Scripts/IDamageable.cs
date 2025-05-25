using System;

public interface IDamageable
{
    public event Action<int, int> OnHealthChange; 
    public event Action OnDeadEvent;
    void ApplyDamage(int damage);
}
