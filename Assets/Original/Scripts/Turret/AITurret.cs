using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret), typeof(SphereCollider))]
public class AITurret : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();
    
    private Turret _turret;

    private Enemy _tempTarget;

    private SphereCollider _trigger;

    private void Awake()
    {
        _turret = GetComponent<Turret>();
        _trigger = GetComponent<SphereCollider>();
        _trigger.isTrigger = true;
        _trigger.radius = _turret.RadiusFire;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (_turret.Target == null)
            {
                _turret.SetTarget(enemy.transform);
                _tempTarget = enemy;
            }

            enemy.OnDeadEvent += SubscriptionOnEnemy;
            
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (_tempTarget == enemy)
            {
                FindNearestTarget();
            }

            UnsubscriptionOnEnemy(enemy);
            
            _enemies.Remove(enemy);
        }
    }

    private void FindNearestTarget()
    {
        Enemy nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Enemy enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            _tempTarget = nearestEnemy;
            _turret.SetTarget(nearestEnemy.transform);
        }
        else
        {
            _tempTarget = null;
            _turret.SetTarget(null);
        }
    }

    private void SubscriptionOnEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        UnsubscriptionOnEnemy(enemy);
        FindNearestTarget();
    }

    private void UnsubscriptionOnEnemy(Enemy enemy)
    {
        enemy.OnDeadEvent -= SubscriptionOnEnemy;
    }
    
}
