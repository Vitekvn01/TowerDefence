using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<TurretData> _turretDatas;
    [SerializeField] private TurretView _turretViewPrefab;
    [SerializeField] private GameObject _itemConteiner;
    [SerializeField] private TurretBuilder _turretBuilder;
    
    private List<TurretView> _turretViews = new List<TurretView>();

    private void Start()
    {
        for (int i = 0; i < _turretDatas.Count; i++)
        {
            AddTurret(_turretDatas[i]);
        }
    }

    private void OnDestroy()
    {
        foreach (var view in _turretViews)
        {
            if (view != null)
            {
                view.SellButtonClick -= OnSellButtonClick;
            }
        }
    }

    private void AddTurret(TurretData turretData)
    {
        var view = Instantiate(_turretViewPrefab, _itemConteiner.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.OnDestroyEvent += OnDestroyView;
        view.Render(turretData);
        _turretViews.Add(view);
    }

    private void OnSellButtonClick(TurretData turretData)
    {
        TrySellTurret(turretData);
    }

    private void OnDestroyView(TurretView view)
    {
        _turretViews.Remove(view);
        view.SellButtonClick -= OnSellButtonClick;
    }

    private void TrySellTurret(TurretData turretData)
    {
        if (turretData.Price <= 1000)
        {
            _turretBuilder.StartPlacement(turretData);
        }
    }
}
