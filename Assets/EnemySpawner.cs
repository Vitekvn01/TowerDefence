using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private List<Wave> _waves;
    
    [SerializeField] private int _count;
    [SerializeField] private float _spawnDelay;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;
    
    [SerializeField] private Enemy _enemyPrefab;

    private void Start()
    {
        Initialize(_enemyPrefab.gameObject);
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
        GetObject(out GameObject spawned);

        Enemy enemy = spawned.GetComponent<Enemy>();

        enemy.transform.position = position;

        enemy.gameObject.SetActive(true);
        
        enemy.Initialize(_target);

        return enemy;
    }
}

[System.Serializable]
public class Wave
{
    public int Count;
    public int Delay;
}