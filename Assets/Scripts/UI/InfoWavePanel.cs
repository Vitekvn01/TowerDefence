using TMPro;
using UnityEngine;

public class InfoWavePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wavesText;
    [SerializeField] private TextMeshProUGUI _enemiesText;

    private int _allEnemies;

    public void ChangeWavesText(int number, int count)
    {
        _wavesText.text = $"{number.ToString()} из {count.ToString()} волн пройдено";
    }
    
    public void ChangeEnemiesText(int number, int count)
    {
        _enemiesText.text = $"{number.ToString()} из {count.ToString()} врагов убито";
    }
}
