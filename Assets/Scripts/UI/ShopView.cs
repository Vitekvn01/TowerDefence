using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private TurretView _turretViewPrefab;
    [SerializeField] private GameObject _itemContainer;
    
    private List<TurretView> _turretViews = new List<TurretView>();
    
    public event Action<TurretData> OnBuyRequested;

    private void OnDestroy()
    {
        foreach (var view in _turretViews)
        {
            view.SellButtonClick -= OnSellButtonClick;
            view.OnDestroyEvent -= OnDestroyView;
            _turretViews.Remove(view);
        }
    }

    public void Render(List<TurretData> turretDatas)
    {
        foreach (var data in turretDatas)
        {
            TurretView view = Instantiate(_turretViewPrefab, _itemContainer.transform);
            view.Render(data);
            view.SellButtonClick += OnSellButtonClick;
            view.OnDestroyEvent += OnDestroyView;
            _turretViews.Add(view);
        }
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private void OnSellButtonClick(TurretData turretData)
    {
        OnBuyRequested?.Invoke(turretData);
    }
    
    private void OnDestroyView(TurretView view)
    {
        _turretViews.Remove(view);
        view.SellButtonClick -= OnSellButtonClick;
        view.OnDestroyEvent -= OnDestroyView;
    }
    

}
