using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : UIElement
{
    [SerializeField] private Button _continiuneButton;
    [SerializeField] private Button _exitButton;
    
    public event Action OnExitClickEvent;
    
    private void Awake()
    {
        _continiuneButton.onClick.AddListener(Hide);
        _exitButton.onClick.AddListener(OnClickExitButton);
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

    private void OnClickExitButton()
    {
        OnExitClickEvent?.Invoke();
    }
}
