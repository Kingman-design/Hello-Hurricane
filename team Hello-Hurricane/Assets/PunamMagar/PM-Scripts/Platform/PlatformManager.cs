using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    [SerializeField] private GameObject platformPrefab;
    public GameObject latestPlatform;

    [SerializeField] Transform spwanTransform;

    [SerializeField] int initialPlatformsToSpwan = 3;

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
        objectPoolerManager = ObjectPoolerManager.Instance;

        for (int i = 0; i < initialPlatformsToSpwan; i++) 
        {
            SpawnNextPlatform();
        }
    }

    private void Update()
    {
        speed += speedIncreaseRate * Time.deltaTime;
    }

    public void SpawnNextPlatform()
    {
        //latestPlatform = Instantiate(platformPrefab, GetEndPosition(_posA), Quaternion.identity, transform);

        Vector3 _posA;
        if (latestPlatformScript != null)
        {
            _posA = latestPlatformScript.GetEndPosition();
        }
        else 
        {
            latestPlatformScript = platformPrefab.GetComponent<Platform>();
            platformModelRenderer = latestPlatformScript.GetModelRenderer();
            _posA = spwanTransform.position;
        }

        string platformTag = objectPoolerManager.GetRandomPlatformTag();

        latestPlatform = objectPoolerManager.SpawnFromPool(platformTag, GetEndPosition(_posA), Quaternion.identity);
        latestPlatformScript = latestPlatform.GetComponent<Platform>();
        latestPlatformScript.tagName = platformTag;
    }

    Vector3 GetEndPosition(Vector3 _posA) 
    {
        Vector3 endPos = Vector3.zero;

        Bounds prefabBound = platformModelRenderer.bounds;

        float halfLength = prefabBound.extents.z;
        endPos.z = _posA.z + halfLength;

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
