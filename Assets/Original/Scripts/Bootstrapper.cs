using System.Collections.Generic;
using IInterfaces;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private SceneData _firstScene;
    [SerializeField] private List<Level> _levels;
    
    private ILevelManager _levelManager;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        _levelManager = new LevelManager(_levels, _firstScene);
        ServiceLocator.Register(_levelManager);

        _levelManager.LoadFirstScene();
    }
}
