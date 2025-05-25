using System;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : UIElement
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _exitButton;

    public event Action OnNextLevelClickEvent;
    public event Action OnExitClickEvent;
    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(OnClickNextLevelButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickNextLevelButton()
    {
        OnNextLevelClickEvent?.Invoke();
    }

    private void OnClickExitButton()
    {
        OnExitClickEvent?.Invoke();
    }
}
