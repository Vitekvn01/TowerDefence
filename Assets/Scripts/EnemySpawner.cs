using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;
    
    [SerializeField] private Enemy _enemyPrefab;

    private ObjectPool _enemyPool;
    
    private Wave _currentWave;
    private int _currentWaveNumber = 0; 
    private int _spawned;
    private bool _isAllSpawned = false;

    private List<Enemy> _spawnEnemies = new List<Enemy>();
    public event Action AllEnemySpawnedEvent;
    public event Action AllEnemyDeadEvent;
    public event Action<int, int> EnemyCountChanged;

    private void Awake()
    {
        _enemyPool = new ObjectPool(_enemyPrefab.gameObject, 50);
    }

    private void Start()
    {
        AllEnemyDeadEvent += NextWave;
        SetWave(0);
        StartCoroutine(SpawnEnemiesRoutine());
    }
    
    public void NextWave() 
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
        StartCoroutine(SpawnEnemiesRoutine());
    }
    
    private void SetWave(int index)
    {
        _isAllSpawned = false;
        _currentWave = _waves[index];
        EnemyCountChanged?.Invoke(0, 1);
    }
    
    private IEnumerator SpawnEnemiesRoutine()
    {
        for (int i = 0; i < _currentWave.Count; i++)
        {
            Enemy spawnedEnemy = SpawnEnemy(_spawnPoint.position);
            _spawned++;
            yield return new WaitForSeconds(_currentWave.Delay);
        }

        _isAllSpawned = true;
        AllEnemySpawnedEvent?.Invoke();
        EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
    }

    private Enemy SpawnEnemy(Vector3 position)
    {
        GameObject spawned = _enemyPool.Get();

        Enemy enemy = spawned.GetComponent<Enemy>();
        enemy.transform.position = position;
        enemy.Initialize(_target);
        AIEnemy aiEnemy = spawned.GetComponent<AIEnemy>();
        aiEnemy.Initialize(_target);
        
        _spawnEnemies.Add(enemy);
        enemy.OnDeadEvent += OnEnemyDead;
        


        return enemy;
    }
    
    private void OnEnemyDead(Enemy enemy)
    {
        enemy.OnDeadEvent -= OnEnemyDead;
        _spawnEnemies.Remove(enemy);
        ChangeSpawnedEnemy();
    }

    private void ChangeSpawnedEnemy()
    {
        _spawned -= 1;
        
        if (_spawned <= 0)
        {
            if (_isAllSpawned)
            {
                AllEnemyDeadEvent?.Invoke();
            }
        }
    }
}

[Serializable]
public class Wave
{
    public GameObject Template;
    public int Count;
    public int Delay;
}