using System.Collections.Generic;
using AbstractFaсtory;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [Header("HUD")] 
    [SerializeField] private InfoWavePanel _infoPanel;
    [SerializeField] private NextWaveButton _nextWaveButton;
    
    [Header("Player")]
    [SerializeField] private int _startMoney;
    
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
