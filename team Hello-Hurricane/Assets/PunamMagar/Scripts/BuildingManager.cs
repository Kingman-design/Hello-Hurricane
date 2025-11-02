using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    Renderer platformRenderer;

    [SerializeField] float zOffsetRange = 2.0f;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        
    }

    public Vector3 GetFinalPosition(GameObject _currBuilding, GameObject _lastBuilding,  Vector3 _offset = default(Vector3), bool isLeftBuilding = false)
    {
        Vector3 endPos = _currBuilding.transform.position;
        Renderer currBuldRenderer = _currBuilding.GetComponent<Building>().GetBuildingRenderer();

        Renderer _lastBuildingRenderer = _lastBuilding.GetComponent<Building>().GetBuildingRenderer();

        if (platformRenderer == null) 
        {
            platformRenderer = PlatformManager.Instance.GetPlatformRenderer();
        }
        
        if (currBuldRenderer != null && platformRenderer != null)
        {
            Bounds lastBuildingBound = _lastBuildingRenderer.bounds;
            Bounds platformBound = platformRenderer.bounds;
            Bounds currBuildingBound = currBuldRenderer.bounds;

            float halfX = currBuildingBound.extents.x + platformBound.extents.x;

            float halfY = currBuildingBound.extents.y - platformBound.extents.y;

            float halfZ = lastBuildingBound.extents.z + currBuildingBound.extents.z;

            if (isLeftBuilding == true)
            {
                endPos.x += halfX;
            }
            else 
            {
                endPos.x -= halfX;
            }
                
            endPos.y += halfY;

            float zOffset = 0f;
            if (_offset.z != 0) 
            {
                zOffset = Random.Range(_offset.z, _offset.z + zOffsetRange);
            }

            endPos.z = _lastBuilding.transform.position.z + halfZ + zOffset;
        }

        return endPos -= _offset;
    }

    public Vector3 GetFinalPosition(GameObject _currBuilding, Vector3 _offset = default(Vector3), bool isLeftBuilding = false)
    {
        Vector3 endPos = PlatformManager.Instance.latestPlatform.transform.position;
        Renderer currBuldRenderer = _currBuilding.GetComponent<Building>().GetBuildingRenderer();

        if (platformRenderer == null)
        {
            platformRenderer = PlatformManager.Instance.GetPlatformRenderer();
        }

        if (currBuldRenderer != null && platformRenderer != null)
        {
            Bounds platformBound = platformRenderer.bounds;
            Bounds currBuildingBound = currBuldRenderer.bounds;

            float halfX = currBuildingBound.extents.x + platformBound.extents.x;

            float halfY = currBuildingBound.extents.y - platformBound.extents.y;

            if (isLeftBuilding == true)
            {
                endPos.x += halfX;
            }
            else
            {
                endPos.x -= halfX;
            }

            endPos.y += halfY;
        }

        return endPos -= _offset;
    }
}
