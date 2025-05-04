using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private float _spawnDelay;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;

    [FormerlySerializedAs("enemyUnitPrefab")] [FormerlySerializedAs("_enemyPrefab")] [SerializeField] private Enemy enemyPrefab;

    private ObjectPool _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool(enemyPrefab.gameObject, 50);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        for (int i = 0; i < _count; i++)
        {
            SpawnEnemy(_spawnPoint.position);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private Enemy SpawnEnemy(Vector3 position)
    {
        GameObject spawned = _enemyPool.Get();

        Enemy enemy = spawned.GetComponent<Enemy>();
        enemy.transform.position = position;
        enemy.gameObject.SetActive(true);

        enemy.Initialize(_target);

        return enemy;
    }
}