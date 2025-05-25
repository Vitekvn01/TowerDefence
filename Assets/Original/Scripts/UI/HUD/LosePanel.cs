using System;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : UIElement
{
    [SerializeField] private Button _restartLevelButton;
    [SerializeField] private Button _exitButton;

    public event Action OnRestartClickEvent;
    public event Action OnExitClickEvent;
    private void Awake()
    {
        _restartLevelButton.onClick.AddListener(OnClickRestartLevelButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickRestartLevelButton()
    {
        OnRestartClickEvent?.Invoke();
    }

    private void OnClickExitButton()
    {
        OnExitClickEvent?.Invoke();
    }
}
