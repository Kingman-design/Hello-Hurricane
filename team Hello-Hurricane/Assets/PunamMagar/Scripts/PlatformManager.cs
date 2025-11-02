using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    [SerializeField] private GameObject platformPrefab;

    public GameObject latestPlatform;
    
    [SerializeField] float platformSpeed = 10f;

    Renderer platformModelRenderer;

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
        if (platformPrefab != null)
        {
            platformModelRenderer = platformPrefab.GetComponent<Platform>().GetModelRenderer();
        }
    }

    public void SpawnNextPlatform(Vector3 _posA)
    {
        latestPlatform = Instantiate(platformPrefab, GetEndPosition(_posA), Quaternion.identity, transform);
    }

    Vector3 GetEndPosition(Vector3 _posA) 
    {
        Vector3 endPos = Vector3.zero;

        if (platformPrefab != null) 
        {
            Bounds prefabBound = platformModelRenderer.bounds;

            float halfLength = prefabBound.extents.z;
            endPos.z = _posA.z + halfLength;
        }

        return endPos;
    }

    public float GetPlatformSpeed() 
    {
        return platformSpeed;
    }

    public Renderer GetPlatformRenderer() 
    {
        return platformModelRenderer;
    }
}
