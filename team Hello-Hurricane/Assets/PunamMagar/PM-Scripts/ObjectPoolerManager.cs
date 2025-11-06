using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolerManager : MonoBehaviour
{
    public static ObjectPoolerManager Instance;

    [System.Serializable]
    public class Pool 
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [Header("Settings")]
    [SerializeField] bool useObstacles = false;

    [SerializeField] List<Pool> buildings;
    [SerializeField] List<Pool> buildingsBG;
    [SerializeField] List<Pool> platforms;
    [SerializeField] List<Pool> obstacles;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
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
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool building in buildings) 
        {
            Queue<GameObject> buildingPool = new Queue<GameObject>();

            for (int i = 0; i < building.size; i++) 
            {
                GameObject obj = Instantiate(building.prefab);
                obj.SetActive(false);
                buildingPool.Enqueue(obj);
            }

            poolDictionary.Add(building.tag, buildingPool);
        }

        //For Background Buildings
        foreach (Pool building in buildingsBG) 
        {
            Queue<GameObject> buildingPool = new Queue<GameObject>();

            for (int i = 0; i < building.size; i++)
            {
                GameObject obj = Instantiate(building.prefab);
                obj.SetActive(false);
                buildingPool.Enqueue(obj);
            }

            poolDictionary.Add(building.tag, buildingPool);
        }

        //For Platforms
        foreach (Pool platform in platforms) 
        {
            Queue<GameObject> platformPool = new Queue<GameObject>();

            for (int i = 0; i < platform.size; i++)
            {
                GameObject obj = Instantiate(platform.prefab);
                obj.SetActive(false);
                platformPool.Enqueue(obj);
            }

            poolDictionary.Add(platform.tag, platformPool);
        }

        //For Obstacles
        if (useObstacles && obstacles != null)
        {
            foreach (Pool obstacle in obstacles)
            {
                Queue<GameObject> obstaclePool = new Queue<GameObject>();
                for (int i = 0; i < obstacle.size; i++)
                {
                    GameObject obj = Instantiate(obstacle.prefab);
                    obj.SetActive(false);
                    obstaclePool.Enqueue(obj);
                }
                poolDictionary.Add(obstacle.tag, obstaclePool);
            }
        }
    }

    public GameObject SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation) 
    {
        if (!poolDictionary.ContainsKey(_tag))
        {
            Debug.LogWarning("Pool with tag " + _tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[_tag].Dequeue();

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = _position;
        objectToSpawn.transform.rotation = _rotation;

        //poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public string GetRandomObjectTag() 
    {
        int randomIndex = Random.Range(0, poolDictionary.Count);
        return poolDictionary.ElementAt(randomIndex).Key;
    }

    public string GetRandomBuildingTag()
    {
        int randomIndex = Random.Range(0, buildings.Count);
        return buildings[randomIndex].tag;
    }

    public string GetRandomBGBuildingTag() 
    {
        int randomIndex = Random.Range(0, buildingsBG.Count);
        return buildingsBG[randomIndex].tag;
    }

    public string GetRandomPlatformTag() 
    {
        int randomIndex = Random.Range(0, platforms.Count);
        return platforms[randomIndex].tag;
    }

    public string GetRandomObstacleTag() 
    {
        int randomIndex = Random.Range(0, obstacles.Count);
        return obstacles[randomIndex].tag;
    }

    public List<string> GetAllObstacleTags() 
    {
        List<string> obstacleTags = new List<string>();
        foreach (Pool obstacle in obstacles) 
        {
            obstacleTags.Add(obstacle.tag);
        }
        return obstacleTags;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
