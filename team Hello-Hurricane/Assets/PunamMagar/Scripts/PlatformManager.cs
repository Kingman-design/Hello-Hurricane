using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    [SerializeField] private GameObject platformPrefab;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SpawnNextPlatform(Vector3 _posA)
    {
        // Instantiate a new platform at the calculated end position
        GameObject newPlatform = Instantiate(platformPrefab, GetEndPosition(_posA), Quaternion.identity);
    }

    Vector3 GetEndPosition(Vector3 _posA) 
    {
        Vector3 endPos = Vector3.zero;

        if (platformPrefab != null) 
        {
            Bounds prefabBound = platformPrefab.GetComponent<Platform>().GetModelRenderer().bounds;

            float halfLength = prefabBound.extents.z;
            endPos.z = _posA.z + halfLength;
        }

        return endPos;
    }
}
