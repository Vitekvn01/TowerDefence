using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIElement _levelsPanel;
    
    [SerializeField] private Button _levelsButton;
    [SerializeField] private Button _exitButton;
    
    private void Awake()
    {
        _levelsButton.onClick.AddListener(_levelsPanel.Show);
        _exitButton.onClick.AddListener(Application.Quit);
    }
    
    
}
