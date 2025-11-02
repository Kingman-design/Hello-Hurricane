using UnityEngine;

public class BuildingSpwaner : MonoBehaviour
{
    public enum BuildingSide
    {
        Right,
        Left
    }

    public BuildingSide Side = BuildingSide.Right;

    [SerializeField] Vector3 buildingOffset;

    [SerializeField] int maxNumOfBuildings = 50;

    ObjectPoolerManager objectPoolerManager;
    PlatformManager platformManager;

    GameObject latestBuilding = null;
    GameObject currBuilding;
    Building currBuildingScript;

    bool hasMaxSpwaned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPoolerManager = ObjectPoolerManager.Instance;
        platformManager = PlatformManager.Instance;

        transform.position = platformManager.latestPlatform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasMaxSpwaned == false) 
        {
            for (int i = 0; i < maxNumOfBuildings; i++)
            {
                SpwanBuilding();
            }
            hasMaxSpwaned = true;
        }
    }

    public void SpwanBuilding() 
    {
        string randTag = objectPoolerManager.GetRandomObjectTag();

        currBuilding = objectPoolerManager.SpawnFromPool(randTag, transform.position, Quaternion.identity);
        currBuilding.GetComponentInParent<Transform>().parent = transform;
        currBuildingScript = currBuilding.GetComponent<Building>();
        currBuildingScript.tagName = randTag;

        Vector3 finalPos = Vector3.zero;

        if (latestBuilding == null)
        {
            // Just place it on the platform with offset
            if (Side == BuildingSide.Left)
            {
                finalPos = BuildingManager.Instance.GetFinalPosition(currBuilding, buildingOffset, true);
            }
            else if (Side == BuildingSide.Right)
            {
                finalPos = BuildingManager.Instance.GetFinalPosition(currBuilding, buildingOffset);
            }
            currBuilding.transform.position = finalPos;

            // Now establish the first building reference
            latestBuilding = currBuilding;
            return;
        }

        if (Side == BuildingSide.Left)
        {
            finalPos = BuildingManager.Instance.GetFinalPosition(currBuilding, latestBuilding, buildingOffset, true);
        }
        else if (Side == BuildingSide.Right)
        {
            finalPos = BuildingManager.Instance.GetFinalPosition(currBuilding, latestBuilding, buildingOffset);
        }

        currBuilding.transform.position = finalPos;

        latestBuilding = currBuilding;
    }
}
