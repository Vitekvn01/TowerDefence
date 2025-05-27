using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lable;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private TurretData _turretData;

    public event Action <TurretData> SellButtonClick;
    public event Action<TurretView> OnDestroyEvent; 

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }

    public void Render (TurretData turretData)
    {
        _turretData = turretData;

        _lable.text = turretData.Label;
        _price.text = turretData.Price.ToString();
        _icon.sprite = turretData.Image;
    }
    

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_turretData);
    }
    
    
}
