using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Image _lockImage;
    
    private bool _isLock;
        
    private Level _level;
    
    public event Action<Level> OnSelectLevelEvent;
    
    private void Awake()
    {
        _selectButton.onClick.AddListener(OnClickHandler);
    }

    private void OnClickHandler()
    {
        OnSelectLevelEvent?.Invoke(_level);
    }

    public void Render(Level level, int number)
    {
        _level = level;
        _text.text = number.ToString();
        
        if (_level.IsLock == false)
        {
            _lockImage.gameObject.SetActive(false);
        }
        else
        {
            _lockImage.gameObject.SetActive(true);
        }
    }
}
