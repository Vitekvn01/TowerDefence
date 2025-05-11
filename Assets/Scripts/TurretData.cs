using UnityEngine;

[CreateAssetMenu(fileName = "New TurretData", menuName = "Turret Data", order = 51)]
public class TurretData : ScriptableObject
{
    [SerializeField] private Turret _turretPrefab;
    
    [SerializeField] private GameObject _previewPrefab;

    [SerializeField] private string _label;
    
    [SerializeField] private int _price;

    public Turret TurretPrefab => _turretPrefab;
    
    public GameObject PreviewPrefab => _previewPrefab;

    public string Label => _label;

    public int Price => _price;
}
