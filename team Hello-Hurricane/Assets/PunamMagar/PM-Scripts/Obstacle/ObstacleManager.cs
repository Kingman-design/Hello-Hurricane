using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    [System.Serializable]
    public struct MinMax 
    {
        public float min;
        public float max;
    }

    [Header("Spwan Settings")]
    [SerializeField] int initialSpwan = 10;
    [SerializeField] int minLanesToSpwan = 1;

    [SerializeField] float spwanDistance = 50f;

    [SerializeField] Transform spwanParent;

    [SerializeField] private List<GameObject> lanes;

    //[Header("")]

    private bool hasInitialSpwaned = false;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //randSpwanTime = Random.Range(spawnInterval.min, spawnInterval.max);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasInitialSpwaned) 
        {
            for (int i = 0; i < initialSpwan; i++)
            {
                SpwanObstacle();
                spwanParent.position += Vector3.forward * spwanDistance;
            }

            spwanParent.position -= Vector3.forward * spwanDistance;
            hasInitialSpwaned = true;
        }
    }

    void ShuffleLanes(List<GameObject> _lanes)
    {
        for (int i = 0; i < _lanes.Count; i++)
        {
            int randIndex = Random.Range(i, _lanes.Count);
            GameObject temp = _lanes[i];
            _lanes[i] = _lanes[randIndex];
            _lanes[randIndex] = temp;
        }
    }

    public void SpwanObstacle()
    {
        ShuffleLanes(lanes);

        int lanesToUse = Random.Range(minLanesToSpwan, lanes.Count + 1);

        for (int i = 0; i < lanesToUse; i++)
        {
            ObstacleSpwaner currSpawner = lanes[i].GetComponent<ObstacleSpwaner>();

            currSpawner.Spwan();
        }
    }
}
