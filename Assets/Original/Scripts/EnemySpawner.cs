using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private const int InitialEnemySize = 200;
    
    private readonly List<Wave> _waves;
    private readonly Transform[] _spawnPoints;
    private readonly DefenceTarget _target;
    private readonly List<Enemy> _enemyPrefabs;

    private Player _player;
    private Wave _currentWave;
    
    private int _currentWaveIndex = 0; 
    private int _spawnedCount;
    private int _deadCount = 0;
    
    private float _spawnTimer = 0f;
    private float _currentDelay;
    
    private bool _isAllSpawned = false;
    private bool _isLastWave = false;
    private bool _isSpawning = false;
    
    private Dictionary<Enemy, ObjectPool> _enemyPools = new Dictionary<Enemy, ObjectPool>();
    private List<Enemy> _spawnEnemies = new List<Enemy>();

    public IReadOnlyList<Wave> Waves => _waves;
    public Wave CurrentWave => _currentWave;
    
    public event Action AllEnemySpawnedEvent;
    public event Action AllEnemyDeadEvent;
    
    public event Action<int, int> OnWaveChanged;
    public event Action<int, int> OnEnemiesChanged;
    public event Action LastWaveStartedEvent;
    public event Action LastWaveCompletedEvent;

    public EnemySpawner(List<Wave> waves, Transform[] spawnPoints, DefenceTarget target, List<Enemy> enemyPrefabs, Player player)
    {
        _waves = waves;
        _spawnPoints = spawnPoints;
        _target = target;
        _enemyPrefabs = enemyPrefabs;
        _player = player;

        foreach (var prefab in _enemyPrefabs)
        {
            _enemyPools[prefab] = new ObjectPool(prefab.gameObject, InitialEnemySize);
        }
        
        AllEnemyDeadEvent += NextWave;
        SetWave(0);
        OnWaveChanged?.Invoke(0, _waves.Count);
    }

    public void StartWave()
    {
        _isSpawning = true;
        _spawnTimer = 0f;
        _currentDelay = _currentWave.GetRandomDelay();
        _spawnedCount = 0;
        _deadCount = 0;    
        
        OnEnemiesChanged?.Invoke(0, _currentWave.Count);

        if (_currentWaveIndex == _waves.Count - 1)
        {
            _isLastWave = true;
            LastWaveStartedEvent?.Invoke();
            Debug.Log("Последняя волна началась");
        }
    }
    
    public void Update()
    {
        if (_isSpawning == true &&  _isAllSpawned == false)
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnedCount < _currentWave.Count && _spawnTimer >= _currentDelay)
            {
                _spawnTimer = 0f;
                _spawnedCount++;

                SpawnEnemy(_spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position);
                _currentDelay = _currentWave.GetRandomDelay();

                if (_spawnedCount >= _currentWave.Count)
                {
                    _isAllSpawned = true;
                    AllEnemySpawnedEvent?.Invoke();
                    
                    _isSpawning = false;
                }
            }
        }
    }
    
    private void NextWave() 
    {
        _currentWaveIndex += 1;
        
        if (_currentWaveIndex <= _waves.Count - 1)
        {
            SetWave(_currentWaveIndex);
            _spawnedCount = 0;
        }
    }
    
    private void SetWave(int index)
    {
        _isAllSpawned = false;
        _currentWave = _waves[index];
    }
    

    private Enemy SpawnEnemy(Vector3 position)
    {
        var prefab = _enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Count)];
        GameObject spawned = _enemyPools[prefab].Get();

        Enemy enemy = spawned.GetComponent<Enemy>();
        enemy.transform.position = position;
        enemy.Initialize(_target);
        AIEnemy aiEnemy = spawned.GetComponent<AIEnemy>();
        aiEnemy.Initialize(_target.transform);
        
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
        _deadCount++;
        
        OnEnemiesChanged?.Invoke(_deadCount, _currentWave.Count);
        
        TryCheckWaveComplete();
    }

    private void TryCheckWaveComplete()
    {
        if (_deadCount == _spawnedCount && _isAllSpawned)
        {
            if (_isLastWave)
            {
                Debug.Log("ПОСЛЕДНЯЯ ВОЛНА завершена");
                LastWaveCompletedEvent?.Invoke();
            }
            
            AllEnemyDeadEvent?.Invoke();
            OnEnemiesChanged?.Invoke(0, _currentWave.Count);
            OnWaveChanged?.Invoke(_currentWaveIndex, _waves.Count);
        }
    }
}

[Serializable]
public class Wave
{
    public int Count;
    
    [Range(0.1f, 5f)]
    public float MinDelay;
    
    [Range(0.1f, 5f)]
    public float MaxDelay;

    public float GetRandomDelay()
    {
       float random = UnityEngine.Random.Range(MinDelay, MaxDelay);
       Debug.Log(random);
       return random;
    }
    

}