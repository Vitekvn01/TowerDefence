using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : UIElement
{
    [SerializeField] private TurretView _turretViewPrefab;
    [SerializeField] private GameObject _itemContainer;
    
    private List<TurretView> _turretViews = new List<TurretView>();
    
    public event Action<TurretData> OnBuyRequested;

    private void OnDestroy()
    {
        foreach (var view in _turretViews)
        {
            if (view != null)
            {
                OnDestroyView(view);  
            }

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
    
    public override void Show()
    {
        base.Show();
        Time.timeScale = 0;
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
    }
    
    private void OnSellButtonClick(TurretData turretData)
    {
        OnBuyRequested?.Invoke(turretData);
    }
    
    private void OnDestroyView(TurretView view)
    {
        view.SellButtonClick -= OnSellButtonClick;
        view.OnDestroyEvent -= OnDestroyView;
    }
    

}
