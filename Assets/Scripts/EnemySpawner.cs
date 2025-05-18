using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;

    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private Transform _target;
    
    [SerializeField] private Enemy _enemyPrefab;

    private Player _player;
    private ObjectPool _enemyPool;
    private Wave _currentWave;
    
    private int _currentWaveIndex = 0; 
    private int _spawned;
    
    private bool _isAllSpawned = false;
    private bool _isLastWave = false;

    private List<Enemy> _spawnEnemies = new List<Enemy>();
    public event Action AllEnemySpawnedEvent;
    public event Action AllEnemyDeadEvent;
    
    public event Action<int, int> OnWaveChanged;
    public event Action<int, int> OnEnemiesChanged;
    public event Action LastWaveStartedEvent;
    public event Action LastWaveCompletedEvent;

    private void Awake()
    {
        _enemyPool = new ObjectPool(_enemyPrefab.gameObject, 1000);
    }

    private void Start()
    {
        AllEnemyDeadEvent += NextWave;
        SetWave(0);
        OnWaveChanged?.Invoke(0, _waves.Count);
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void StartWave()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        
        if (_currentWaveIndex == _waves.Count - 1)
        {
            _isLastWave = true;
            LastWaveStartedEvent?.Invoke();
            Debug.Log("Послденяя волна началась");
        }
    }
    private void NextWave() 
    {
        _currentWaveIndex += 1;
        
        if (_currentWaveIndex <= _waves.Count - 1)
        {
            SetWave(_currentWaveIndex);
            _spawned = 0;
        }
    }
    
    private void SetWave(int index)
    {
        _isAllSpawned = false;
        _currentWave = _waves[index];
    }
    
    private IEnumerator SpawnEnemiesRoutine()
    {
        OnEnemiesChanged?.Invoke(0, _currentWave.Count);
        
        for (int i = 0; i < _currentWave.Count; i++)
        {
            _spawned++;
            Enemy spawnedEnemy = SpawnEnemy(_spawnPoint[Random.Range(0, _spawnPoint.Length)].position);
            yield return new WaitForSeconds(_currentWave.Delay);
        }

        _isAllSpawned = true;
        AllEnemySpawnedEvent?.Invoke();
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
        enemy.OnDeadEvent += OnEnemyReward;
        return enemy;
    }
    
    private void OnEnemyDead(Enemy enemy)
    {
        enemy.OnDeadEvent -= OnEnemyDead;
        _spawnEnemies.Remove(enemy);
        ChangeSpawnedEnemy();
    }

    private void OnEnemyReward(Enemy enemy)
    {
        enemy.OnDeadEvent -= OnEnemyReward;
        _player.Wallet.AddMoney(enemy.Reward);
    }

    private void ChangeSpawnedEnemy()
    {
        _spawned--;

        if (_spawned <= 0 && _isAllSpawned)
        {
            OnEnemiesChanged?.Invoke(0, _currentWave.Count);
            
            if (_isLastWave)
            {
                Debug.Log("ПОСЛЕДНЯЯ ВОЛНА завершена");
                LastWaveCompletedEvent?.Invoke();
            }
            
            AllEnemyDeadEvent?.Invoke();
            
            OnWaveChanged?.Invoke(_currentWaveIndex, _waves.Count);
        }
    }
}

[Serializable]
public class Wave
{
    public int Count;
    public float Delay;
}