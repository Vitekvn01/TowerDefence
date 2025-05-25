using System.Collections.Generic;
using IInterfaces;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : UIElement
{
    [SerializeField] private GameObject _container;
    [SerializeField] private LevelView _levelView;
    [SerializeField] private Button _closeButton;
    
    private List<LevelView> _levelViews = new List<LevelView>();
    
    private ILevelManager _levelManager;

    public void Initialize(ILevelManager levelManager)
    {
        _levelManager = levelManager;
        Render(levelManager.GetLevels());
        Debug.Log("init LevelPanel");
        _closeButton.onClick.AddListener(Hide);
    }
    
    private void Render(IReadOnlyList<Level> levels)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            LevelView view = Instantiate(_levelView.gameObject, _container.transform).GetComponent<LevelView>();

            _levelViews.Add(view);
            
            int levelIndex = i + 1;
            
            view.Render(levels[i], levelIndex);

            view.OnSelectLevelEvent += _levelManager.LoadLevel;
            
            Debug.Log("init level view" + i);
        }
    }
    
    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(Hide);
        
        foreach (var view in _levelViews)
        {
            if (view != null)
            {
                view.OnSelectLevelEvent -= _levelManager.LoadLevel;
            }
        }
    }
}
