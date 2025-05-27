using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private int _explosionDamage = 50;
    [SerializeField] private LayerMask _damageLayerMask;
    [SerializeField] private ExplosionEffect _explosionEffect;
    
    private ObjectPool _explosionPool;

    private void Awake()
    {
        _explosionPool = new ObjectPool(_explosionEffect.gameObject, 1);
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        Explode();
        base.OnTriggerEnter(other);
    }

    private void Explode()
    {
        GameObject explosion = _explosionPool.Get();
        explosion.SetActive(true);
        explosion.transform.position = transform.position;

        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius, _damageLayerMask, QueryTriggerInteraction.Ignore);
        
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(_explosionDamage);
            }
        }
    }
}
