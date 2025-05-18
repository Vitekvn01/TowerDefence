using AbstractFaсtory;
using IInterfaces;
using UnityEngine;

public class TurretBuilder
{
    private const float PlaceRadius = 1;
    
    private readonly LayerMask _placementLayerMask;
    private readonly IFactory<Turret> _factory;
    private readonly IWallet _wallet;

    private TurretData _selectedData;
    private GameObject _currentPreview;
    
    
    public TurretBuilder(LayerMask placementLayer, IFactory<Turret> factory, IWallet wallet)
    {
        _placementLayerMask = placementLayer;
        _factory = factory;
        _wallet = wallet;
    }

    public void StartPlacement(TurretData data)
    {
        Debug.Log("StartPlacement");
        _selectedData = data;
        _currentPreview = Object.Instantiate(data.PreviewPrefab);
    }

    public void Update()
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
        Object.Destroy(_currentPreview);
        _selectedData = null;
    }
}
