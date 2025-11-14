using UnityEngine;

public class BuildingSpwaner : MonoBehaviour
{
    public enum BuildingSide
    {
        Right,
        Left
    }

    public enum BuildingType 
    {
        Foreground,
        Background
    }

    public BuildingSide Side = BuildingSide.Right;
    public BuildingType Type = BuildingType.Foreground;

    [SerializeField] Vector3 buildingOffset;

    [SerializeField] int maxNumOfBuildings = 50;

    ObjectPoolerManager objectPoolerManager;
    PlatformManager platformManager;
    BuildingManager buildingManager;

    GameObject latestBuilding = null;
    GameObject currBuilding;
    Building currBuildingScript;

    //bool hasMaxSpwaned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPoolerManager = ObjectPoolerManager.Instance;
        buildingManager = BuildingManager.Instance;

        //transform.position = platformManager.latestPlatform.transform.position;
    }

    private void OnEnable()
    {
        PlatformManager.OnPlatformSpawned += SpwanInitialPlatforms;
    }

    private void OnDisable()
    {
        PlatformManager.OnPlatformSpawned -= SpwanInitialPlatforms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpwanBuilding() 
    {
        if (objectPoolerManager == null) 
        {
            objectPoolerManager = ObjectPoolerManager.Instance;
        }

        if (buildingManager == null) 
        {
            buildingManager = BuildingManager.Instance;
        }

        string randTag = "";
        if (Type == BuildingType.Foreground)
        {
            randTag = objectPoolerManager.GetRandomBuildingTag();
        }
        else if (Type == BuildingType.Background) 
        {
            randTag = objectPoolerManager.GetRandomBGBuildingTag();
        }
            

        currBuilding = objectPoolerManager.SpawnFromPool(randTag, transform.position, Quaternion.identity);
        currBuildingScript = currBuilding.GetComponent<Building>();
        currBuildingScript.tagName = randTag;
        currBuildingScript.buildingSpwaner = this;

        Vector3 finalPos = Vector3.zero;

        // If this is the first building to be placed`
        if (latestBuilding == null)
        {
            if (Side == BuildingSide.Left)
            {
                finalPos = buildingManager.GetFinalPosition(currBuilding, buildingOffset, true);
            }
            else if (Side == BuildingSide.Right)
            {
                finalPos = buildingManager.GetFinalPosition(currBuilding, buildingOffset);
            }
            currBuilding.transform.position = finalPos;

            latestBuilding = currBuilding;
            return;
        }

        if (Side == BuildingSide.Left)
        {
            finalPos = buildingManager.GetFinalPosition(currBuilding, latestBuilding, buildingOffset, true);
        }
        else if (Side == BuildingSide.Right)
        {
            finalPos = buildingManager.GetFinalPosition(currBuilding, latestBuilding, buildingOffset);
        }

        currBuilding.transform.position = finalPos;

        latestBuilding = currBuilding;
    }

    void SpwanInitialPlatforms() 
    {
        for (int i = 0; i < maxNumOfBuildings; i++)
        {
            SpwanBuilding();
        }
    }
}
