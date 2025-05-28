using System;
using System.Collections.Generic;
using IInterfaces;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private SceneData _firstScene;
    [SerializeField] private List<Level> _levels;
    
    private ILevelManager _levelManager;
    private ADController _adController;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _adController = new ADController();
        _levelManager = new LevelManager(_levels, _firstScene);
        _levelManager.OnLoadLevelEvent += _adController.ShowInterstitial;
        ServiceLocator.Register(_levelManager);

        _levelManager.LoadFirstScene();
    }

    private void OnDestroy()
    {
        _levelManager.OnLoadLevelEvent -= _adController.ShowInterstitial;
    }
}
