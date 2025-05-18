using System;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    private Button _button;

    public event Action OnClickButton; 
    
    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnClickButtonEvent);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnClickButtonEvent()
    {
        OnClickButton?.Invoke();
        Hide();
    }
}
