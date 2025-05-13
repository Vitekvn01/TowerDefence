using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<TurretData> _turretDatas;
    [SerializeField] private TurretBuilder _turretBuilder;
    [SerializeField] private ShopView _shopView;

    private void Awake()
    {
        _shopView.OnBuyRequested += TrySellTurret;
    }

    private void Start()
    {
        _shopView.Render(_turretDatas);
    }

    private void OnDestroy()
    {
        _shopView.OnBuyRequested -= TrySellTurret;
    }
    
    private void TrySellTurret(TurretData turretData)
    {
        if (turretData.Price <= 1000)
        {
            _turretBuilder.StartPlacement(turretData);
            _shopView.Hide();
        }
    }
}
