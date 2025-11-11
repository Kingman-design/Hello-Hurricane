using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;

public class ObjectPoolerManager : MonoBehaviour
{
    public static ObjectPoolerManager Instance;

    public static event Action OnDoneAddingToDictionary;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [Header("Settings")]
    public bool usePunamObstacles = false;

    [SerializeField] List<Pool> buildings;
    [SerializeField] List<Pool> buildingsBG;
    [SerializeField] List<Pool> platforms;
    [SerializeField] List<Pool> obstacles;
    [SerializeField] List<Pool> nextObstacleTriggers;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Start()
    {
        ResetObjectPooling();
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
        int randomIndex = UnityEngine.Random.Range(0, poolDictionary.Count);
        return poolDictionary.ElementAt(randomIndex).Key;
    }

    public string GetRandomBuildingTag()
    {
        int randomIndex = UnityEngine.Random.Range(0, buildings.Count);
        return buildings[randomIndex].tag;
    }

    public string GetRandomBGBuildingTag() 
    {
        int randomIndex = UnityEngine.Random.Range(0, buildingsBG.Count);
        return buildingsBG[randomIndex].tag;
    }

    public string GetRandomPlatformTag() 
    {
        int randomIndex = UnityEngine.Random.Range(0, platforms.Count);
        return platforms[randomIndex].tag;
    }

    public string GetRandomObstacleTag() 
    {
        int randomIndex = UnityEngine.Random.Range(0, obstacles.Count);
        return obstacles[randomIndex].tag;
    }

    public string GetNextObstacleTriggerTag() 
    {
        int randomIndex = UnityEngine.Random.Range(0, nextObstacleTriggers.Count);
        return nextObstacleTriggers[randomIndex].tag;
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

    void AddToDictionary(Pool _object) 
    {
        Queue<GameObject> objPool = new Queue<GameObject>();
        for (int i = 0; i < _object.size; i++)
        {
            GameObject obj = Instantiate(_object.prefab);
            obj.SetActive(false);
            objPool.Enqueue(obj);
        }

        poolDictionary.Add(_object.tag, objPool);
    }

    void InvokeSpwaning() 
    {
        OnDoneAddingToDictionary?.Invoke();
    }

    public void ResetObjectPooling()
    {
        if (poolDictionary == null)
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
        }
        else 
        {
            poolDictionary.Clear();
        }

        foreach (Pool building in buildings)
        {
            AddToDictionary(building);
        }

        //For Background Buildings
        foreach (Pool building in buildingsBG)
        {
            AddToDictionary(building);
        }

        //For Platforms
        foreach (Pool platform in platforms)
        {
            AddToDictionary(platform);
        }

        //For Obstacles
        if (usePunamObstacles && obstacles.Count != 0)
        {
            foreach (Pool obstacle in obstacles)
            {
                AddToDictionary(obstacle);
            }

            //For Next Obstacle Triggers
            foreach (Pool nextObstacleTrigger in nextObstacleTriggers)
            {
                AddToDictionary(nextObstacleTrigger);
            }
        }

        InvokeSpwaning();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetObjectPooling();
    }
}
