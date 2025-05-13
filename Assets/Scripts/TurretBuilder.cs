using AbstractFaсtory;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{
    private const float PlaceRadius = 1;
    
    [SerializeField] private LayerMask _placementLayerMask;
    
    private IFactory<Turret> _factory;
    private TurretData _selectedData;
    private GameObject _currentPreview;
    
    
    public void Initialize(IFactory<Turret> factory)
    {
        _factory = factory;
    }

    public void StartPlacement(TurretData data)
    {
        Debug.Log("StartPlacement");
        _selectedData = data;
        _currentPreview = Instantiate(data.PreviewPrefab);
    }

    private void Start()
    {
        Initialize(new TurretFactory<Turret>());
    }

    private void Update()
    {
        if (_selectedData != null)
        {
            Debug.Log("Upd");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _placementLayerMask, QueryTriggerInteraction.Ignore))
            {
                _currentPreview.transform.position = hit.point;
                
                /*Debug.Log(hit.point);*/
                
                if (CanPlaceTurret(_currentPreview.transform.position, PlaceRadius) && Input.GetMouseButtonDown(0))
                {
                    PlaceTurret(hit.point);
                }
            }
        }
    }
    
    private bool CanPlaceTurret(Vector3 position, float radius)
    {
        bool canPlace = false;
        
        int mask = ~_placementLayerMask.value;
        Collider[] overlaps = Physics.OverlapSphere(position, radius, mask, QueryTriggerInteraction.Ignore);
        
        foreach (var VARIABLE in overlaps)
        {
            Debug.Log(VARIABLE.gameObject.name);
        }
        
        if (overlaps.Length == 0)
        {

            canPlace = true;
        }
        else
        {

            
            canPlace = false;
        }
        Debug.Log("canPlace: " + canPlace);
        return canPlace;
    }
    

    private void PlaceTurret(Vector3 position)
    {
        Debug.Log("Place");
        _factory.Create(_selectedData.TurretPrefab, position, Quaternion.identity);
        Destroy(_currentPreview);
        _selectedData = null;
    }
}
