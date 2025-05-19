using System.Collections.Generic;
using AbstractFaсtory;
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
    
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CountTextView _countScoreView;
    
    [Header("Shop")] 
    [SerializeField] private CountTextView _countMoneyView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private List<TurretData> _turretDatas;
    
    [Header("Turret Builder")]
    [SerializeField] private LayerMask _placementLayerMask;
    
    private Player _player;
    private TurretBuilder _turretBuilder;
    private Shop _shop;
    private GameResultHandler _gameResultHandler;
    
    private void Awake()
    {
        Wallet wallet = new Wallet(_startMoney);
        _countMoneyView.ChangeCountText(wallet.Money);
        wallet.OnChangeCountEvent += _countMoneyView.ChangeCountText;
        Score score = new Score();
        _player = new Player(wallet, score);

        _nextWaveButton.OnClickButton += _enemySpawner.StartWave;
        
        _enemySpawner.SetPlayer(_player);
        _enemySpawner.OnEnemiesChanged += _infoPanel.ChangeEnemiesText;
        _enemySpawner.OnWaveChanged += _infoPanel.ChangeWavesText;
        _enemySpawner.AllEnemyDeadEvent += _nextWaveButton.Show;
        
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
    }

    private void OnDestroy()
    {
        _shop.Dispose();
        _player.Wallet.OnChangeCountEvent -= _countMoneyView.ChangeCountText;
        
        _nextWaveButton.OnClickButton -= _enemySpawner.StartWave;
        
        _enemySpawner.OnEnemiesChanged -= _infoPanel.ChangeEnemiesText;
        _enemySpawner.OnWaveChanged -= _infoPanel.ChangeEnemiesText;
        _enemySpawner.AllEnemyDeadEvent -= _nextWaveButton.Show;
    }
}
