using System.Collections.Generic;
using IInterfaces;

public class Shop
{
    private readonly List<TurretData> _turretDatas;
    private readonly TurretBuilder _turretBuilder;
    private readonly ShopView _shopView;
    private readonly IWallet _wallet;

    public Shop(List<TurretData> turretDatas, TurretBuilder turretBuilder, ShopView shopView, IWallet wallet)
    {
        _turretDatas = turretDatas;
        _turretBuilder = turretBuilder;
        _shopView = shopView;
        _wallet = wallet;
        
        _shopView.OnBuyRequested += TrySellTurret;
        _shopView.Render(_turretDatas);
    }

    public void Dispose()
    {
        _shopView.OnBuyRequested -= TrySellTurret;
    }

    private void TrySellTurret(TurretData turretData)
    {
        if (turretData.Price <= _wallet.Money)
        {
            _wallet.RemoveMoney(turretData.Price);
            _turretBuilder.StartPlacement(turretData);
            _shopView.Hide();
        }
    }
}
