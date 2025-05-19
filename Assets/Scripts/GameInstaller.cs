using System.Collections.Generic;
using AbstractFaсtory;
using Unity.AI.Navigation;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    
    [Header("UI")] 
    [SerializeField] private InfoWavePanel _infoPanel;
    [SerializeField] private NextWaveButton _nextWaveButton;
    [SerializeField] private UIElement _winPanel;
    [SerializeField] private UIElement _losePanel;
    
    [Header("Player")]
    [SerializeField] private int _startMoney;
    [SerializeField] private DefenceTarget _defenceTarget;
    
    [Header("EnemySpawner")]
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Enemy _enemyPrefab;
    
    [Header("Shop")] 
    [SerializeField] private CountTextView _countMoneyView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private List<TurretData> _turretDatas;
    
    [Header("Turret Builder")]
    [SerializeField] private LayerMask _placementLayerMask;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    
    private Player _player;
    private EnemySpawner _enemySpawner;
    private Shop _shop;
    private TurretBuilder _turretBuilder;
    private GameResultHandler _gameResultHandler;

    
    private void Awake()
    {
        Wallet wallet = new Wallet(_startMoney);
        _countMoneyView.ChangeCountText(wallet.Money);
        wallet.OnChangeCountEvent += _countMoneyView.ChangeCountText;
        Score score = new Score();
        _player = new Player(wallet, score);
        
        _enemySpawner = new EnemySpawner(_waves, _spawnPoints, _defenceTarget, _enemyPrefab, _player);
        _enemySpawner.OnEnemiesChanged += _infoPanel.ChangeEnemiesText;
        _enemySpawner.OnWaveChanged += _infoPanel.ChangeWavesText;
        _enemySpawner.AllEnemyDeadEvent += _nextWaveButton.Show;
        
        _infoPanel.Init(_enemySpawner);
        _nextWaveButton.OnClickButton += _enemySpawner.StartWave;
        
        _gameResultHandler = new GameResultHandler(_winPanel, _losePanel);
        
        _enemySpawner.LastWaveCompletedEvent += _gameResultHandler.Win;
        _defenceTarget.OnDeadEvent += _gameResultHandler.Lose;
        
        IFactory<Turret> turretFactory = new TurretFactory<Turret>();
        _turretBuilder = new TurretBuilder(_placementLayerMask, turretFactory, wallet);
        _shop = new Shop(_turretDatas, _turretBuilder, _shopView, wallet);
    }

    private void Update()
    {
        _turretBuilder.Update();
        _enemySpawner.Update();
    }

    private void OnDestroy()
    {
        _shop.Dispose();
        _player.Wallet.OnChangeCountEvent -= _countMoneyView.ChangeCountText;
        
        _nextWaveButton.OnClickButton -= _enemySpawner.StartWave;
        
        _enemySpawner.OnEnemiesChanged -= _infoPanel.ChangeEnemiesText;
        _enemySpawner.OnWaveChanged -= _infoPanel.ChangeEnemiesText;
        _enemySpawner.AllEnemyDeadEvent -= _nextWaveButton.Show;
        
        _enemySpawner.LastWaveCompletedEvent -= _gameResultHandler.Win;
        _defenceTarget.OnDeadEvent -= _gameResultHandler.Lose;
    }
}
