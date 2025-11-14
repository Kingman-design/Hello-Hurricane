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

        DontDestroyOnLoad(gameObject);
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

            currBuildingBound.center = Vector3.zero;

            float halfX = currBuildingBound.extents.x + platformBound.extents.x;

            float halfY = platformBound.extents.y;

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
            //zOffset = Random.Range(_offset.z, _offset.z + zOffsetRange);

            endPos.x -= _offset.x;

            endPos.z += _lastBuilding.transform.position.z + halfZ + zOffset;
            endPos.z += _offset.z;
        }

        return endPos;
    }

    public Vector3 GetFinalPosition(GameObject _currBuilding, Vector3 _offset = default(Vector3), bool isLeftBuilding = false)
    {
        Vector3 endPos = _currBuilding.transform.position;
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

            float halfY = platformBound.extents.y;

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
