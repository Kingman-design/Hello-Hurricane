using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    [SerializeField] private GameObject platformPrefab;
    public GameObject latestPlatform;

    [SerializeField] Transform spwanTransform;

    Renderer platformModelRenderer;

    [Header("Speed Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float speedIncreaseRate = 0.1f;

    Platform latestPlatformScript;

    ObjectPoolerManager objectPoolerManager;
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

        objectPoolerManager = ObjectPoolerManager.Instance;
    }

    private void Update()
    {
        speed += speedIncreaseRate * Time.deltaTime;
    }

    public void SpawnNextPlatform(Vector3 _posA)
    {
        //latestPlatform = Instantiate(platformPrefab, GetEndPosition(_posA), Quaternion.identity, transform);

        string platformTag = objectPoolerManager.GetRandomPlatformTag();

        latestPlatform = objectPoolerManager.SpawnFromPool(platformTag, spwanTransform.position, Quaternion.identity);
        latestPlatformScript = latestPlatform.GetComponent<Platform>();
        latestPlatformScript.tagName = platformTag;
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

    public float GetSpeed() 
    {
        return speed;
    }

    public Renderer GetPlatformRenderer() 
    {
        return platformModelRenderer;
    }
}
