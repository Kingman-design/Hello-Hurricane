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

        //For random rotation
        currBuldRenderer.gameObject.transform.localRotation = GetRandomRotation();

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

            float halfY = platformBound.extents.y + currBuildingBound.extents.y;

            float halfZ = lastBuildingBound.extents.z + currBuildingBound.extents.z;

            endPos.x = platformRenderer.transform.position.x + (isLeftBuilding ? +halfX : -halfX);

            endPos.y += halfY;

            float zOffset = 0f;
            zOffset = Random.Range(_offset.z, _offset.z + zOffsetRange);

            endPos.x -= _offset.x;

            endPos.z += _lastBuilding.transform.position.z + halfZ + zOffset;
        }
        return endPos;
    }

    public Vector3 GetFinalPosition(GameObject _currBuilding, Vector3 _offset = default(Vector3), bool isLeftBuilding = false)
    {
        Vector3 endPos = _currBuilding.transform.position;
        Renderer currBuldRenderer = _currBuilding.GetComponent<Building>().GetBuildingRenderer();

        //For random rotation
        currBuldRenderer.gameObject.transform.localRotation = GetRandomRotation();

        if (platformRenderer == null)
        {
            platformRenderer = PlatformManager.Instance.GetPlatformRenderer();
        }

        if (currBuldRenderer != null && platformRenderer != null)
        {
            Bounds platformBound = platformRenderer.bounds;
            Bounds currBuildingBound = currBuldRenderer.bounds;

            float halfX = currBuildingBound.extents.x + platformBound.extents.x;

            float halfY = platformBound.extents.y + currBuildingBound.extents.y;

            endPos.x = platformRenderer.transform.position.x + (isLeftBuilding ? +halfX : -halfX);

            endPos.x -= _offset.x;

            endPos.y += halfY;
        }

        return endPos;
    }


    float[] rotationAngles = { 0f, 90f, 180f, 270f };
    public Quaternion GetRandomRotation()
    {
        int index = Random.Range(0, rotationAngles.Length);
        return Quaternion.Euler(0f, rotationAngles[index], 0f);
    }
}
