using System.Collections.Generic;
using System.Linq;
using AbstractFaсtory;
using IInterfaces;
using Unity.AI.Navigation;
using UnityEngine;

public class TurretBuilder
{
    private const float PlaceRadius = 1;
    
    private readonly LayerMask _placementLayerMask;
    private readonly IFactory<Turret> _factory;
    private readonly Vector3 _buildPoint;
    private readonly DrawerCircle _drawerCircleBuldZone;
    private readonly DrawerCircle _drawerCircleFireZone;

    private float _buildRadius;
    
    private TurretData _selectedData;
    private GameObject _currentPreview;
    
    private Color _canPlaceColor = Color.green;
    private Color _cantPlaceColor = Color.red;

    private List<Material> _previewMaterials = new List<Material>();
    
    
    public TurretBuilder(LayerMask placementLayer, IFactory<Turret> factory, Vector3 buildPoint, float buildRadius, DrawerCircle drawerCirclePrefab)
    {
        _placementLayerMask = placementLayer;
        _factory = factory;
        _buildPoint = buildPoint;
        _buildRadius = buildRadius;

        _drawerCircleBuldZone = Object.Instantiate(drawerCirclePrefab.gameObject, buildPoint, Quaternion.identity).GetComponent<DrawerCircle>();
        _drawerCircleBuldZone.SetRadius(_buildRadius);
        _drawerCircleBuldZone.SetColor(_cantPlaceColor);
        _drawerCircleBuldZone.gameObject.SetActive(false);
        
        _drawerCircleFireZone = Object.Instantiate(drawerCirclePrefab.gameObject, buildPoint, Quaternion.identity).GetComponent<DrawerCircle>();
        _drawerCircleFireZone.SetColor(_cantPlaceColor);
        _drawerCircleFireZone.gameObject.SetActive(false);
    }

    public void StartPlacement(TurretData data)
    {
        Debug.Log("StartPlacement");
        
        _drawerCircleBuldZone.gameObject.SetActive(true);
        
        _selectedData = data;
        _currentPreview = Object.Instantiate(data.PreviewPrefab);
        _drawerCircleFireZone.SetRadius(_selectedData.TurretPrefab.RadiusFire);
        _drawerCircleFireZone.gameObject.SetActive(true);
        TryGetMaterials();
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
                
                _drawerCircleFireZone.transform.position = hit.point;

                if (CanPlaceTurret(_currentPreview.transform.position, PlaceRadius))
                {
                    ChangeColor(_canPlaceColor);
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTurret(hit.point, hit.transform);
                    }
                }
                else
                {
                    ChangeColor(_cantPlaceColor);
                }
            }
        }
    }



    private bool CanPlaceTurret(Vector3 position, float radius)
    {
        bool canPlace = false;
        
        if (Vector3.Distance(position, _buildPoint) < _buildRadius)
        {
            int mask = ~_placementLayerMask.value;
            
            Collider[] overlaps = Physics.OverlapSphere(position, radius, mask, QueryTriggerInteraction.Ignore);
        
            if (overlaps.Length == 0)
            {
                canPlace = true;
            }
        }

        Debug.Log("canPlace: " + canPlace);
        return canPlace;
    }
    
    private void ChangeColor(Color color)
    {
        if (_previewMaterials.Count != 0)
        {
            foreach (var material in _previewMaterials)
            {
                material.color = color;
            }
        }
    }
    
    private void PlaceTurret(Vector3 position, Transform parent = null)
    {
        Debug.Log("Place");
        Object.Destroy(_currentPreview);
        Turret createTurret = _factory.Create(_selectedData.TurretPrefab, position, Quaternion.identity);
        createTurret.transform.parent = parent;
        _selectedData = null;
        _drawerCircleBuldZone.gameObject.SetActive(false);
        _drawerCircleFireZone.gameObject.SetActive(false);
        
    }
    
    private void TryGetMaterials()
    {
        List<Renderer> renderer = _currentPreview.GetComponentsInChildren<Renderer>().ToList();

        if (renderer.Count != 0)
        {
            int index = 0;
            foreach (Renderer render in renderer)
            {
                _previewMaterials.Add(render.material);
                index++;
            }
        }
    }
}
