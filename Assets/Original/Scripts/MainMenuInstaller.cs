using IInterfaces;
using UnityEngine;

public class MainMenuInstaller : MonoBehaviour
{
    [SerializeField] private LevelPanel _levelPanel;

    private ILevelManager _levelManager;
    
    private void Awake()
    {
        _levelManager = ServiceLocator.Get<ILevelManager>();
        _levelPanel.Initialize(_levelManager);
    }
}
