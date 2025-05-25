using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider Slider;

    private DefenceTarget _defenceTarget;
    
    public void Initialize(DefenceTarget defenceTarget)
    {
        _defenceTarget = defenceTarget; 
        
        OnValueChanged(_defenceTarget.Health, _defenceTarget.Health);
        
        _defenceTarget.OnHealthChange += OnValueChanged;
    }
    private void OnValueChanged(int value, int maxValue)
    {
        Debug.Log("Value:" + value + " maxValue:" + maxValue);
        Slider.value = (float)value / maxValue;
    }

    private void OnDestroy()
    {
        _defenceTarget.OnHealthChange -= OnValueChanged;
    }
}
