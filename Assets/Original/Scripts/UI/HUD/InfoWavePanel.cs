using TMPro;
using UnityEngine;

public class InfoWavePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wavesText;
    [SerializeField] private TextMeshProUGUI _enemiesText;
    
    public void Initialize(EnemySpawner _enemySpawner)
    {
        ChangeEnemiesText(0, _enemySpawner.CurrentWave.Count);
        ChangeWavesText(0, _enemySpawner.Waves.Count);
    }
    public void ChangeWavesText(int number, int count)
    {
        _wavesText.text = $"{number.ToString()} из {count.ToString()} волны пройдено";
    }
    
    public void ChangeEnemiesText(int number, int count)
    {
        _enemiesText.text = $"{number.ToString()} из {count.ToString()} убито";
    }
}
