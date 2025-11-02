using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    [SerializeField] List<Pool> buildings;
    //[SerializeField] List<Pool> obstacles;

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

    public void ReturnToPool(string tag, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
