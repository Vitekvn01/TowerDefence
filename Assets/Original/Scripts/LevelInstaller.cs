using System.Collections.Generic;
using AbstractFaсtory;
using IInterfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelInstaller : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private InfoWavePanel _infoPanel;
    [SerializeField] private NextWaveButton _nextWaveButton;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private HealthBar _healthBar;
    
    [Header("Player")]
    [SerializeField] private int _startMoney;
    [SerializeField] private DefenceTarget _defenceTarget;
    
    [Header("EnemySpawner")]
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private List<Enemy> _enemyPrefabs;
    
    [Header("Shop")] 
    [SerializeField] private CountTextView _moneyView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private List<TurretData> _turretDatas;
    
    [Header("Turret Builder")]
    [SerializeField] private LayerMask _placementLayerMask;
    [SerializeField] private Transform _buildPoint;
    [SerializeField] private float _buildRadius;
    [SerializeField] private DrawerCircle _drawerCirclePrefab;
    
    private Player _player;
    private EnemySpawner _enemySpawner;
    private Shop _shop;
    private TurretBuilder _turretBuilder;
    private GameResultHandler _gameResultHandler;
    private ILevelManager _levelManager;
    
    private void Awake()
    {
        Wallet wallet = new Wallet(_startMoney);
        _moneyView.ChangeCountText(wallet.Money);
        wallet.OnChangeCountEvent += _moneyView.ChangeCountText;
        _player = new Player(wallet);
        
        _enemySpawner = new EnemySpawner(_waves, _spawnPoints, _defenceTarget, _enemyPrefabs, _player);
        _enemySpawner.OnEnemiesChanged += _infoPanel.ChangeEnemiesText;
        _enemySpawner.OnWaveChanged += _infoPanel.ChangeWavesText;
        _enemySpawner.AllEnemyDeadEvent += _nextWaveButton.Show;
        
        _infoPanel.Initialize(_enemySpawner);
        _nextWaveButton.OnClickButton += _enemySpawner.StartWave;

        
        _levelManager = ServiceLocator.Get<ILevelManager>();
        
        _enemySpawner.LastWaveCompletedEvent += _levelManager.UnlockNextLevel;
        _pauseMenu.OnExitClickEvent += _levelManager.LoadFirstScene;
        
        _gameResultHandler = new GameResultHandler(_winPanel, _losePanel, _levelManager);
        _enemySpawner.LastWaveCompletedEvent += _gameResultHandler.Win;
        
        _defenceTarget.OnDeadEvent += _gameResultHandler.Lose;
        
        _healthBar.Initialize(_defenceTarget);
        
        IFactory<Turret> turretFactory = new TurretFactory<Turret>();
        _turretBuilder = new TurretBuilder(_placementLayerMask, turretFactory, _buildPoint.position, _buildRadius, _drawerCirclePrefab);
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
        _player.Wallet.OnChangeCountEvent -= _moneyView.ChangeCountText;
        
        _nextWaveButton.OnClickButton -= _enemySpawner.StartWave;
        
        _enemySpawner.OnEnemiesChanged -= _infoPanel.ChangeEnemiesText;
        _enemySpawner.OnWaveChanged -= _infoPanel.ChangeEnemiesText;
        _enemySpawner.AllEnemyDeadEvent -= _nextWaveButton.Show;
        
        _enemySpawner.LastWaveCompletedEvent -= _gameResultHandler.Win;
        _defenceTarget.OnDeadEvent -= _gameResultHandler.Lose;
        _pauseMenu.OnExitClickEvent -= _levelManager.LoadFirstScene;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_buildPoint == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_buildPoint.position, _buildRadius);
    }
#endif
}
