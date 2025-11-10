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
    ObjectPoolerManager poolerManager;

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
        poolerManager = ObjectPoolerManager.Instance;

        gameObject.SetActive(poolerManager.useObstacles);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasInitialSpwaned)
        {
            for (int i = 0; i < initialSpwan; i++)
            {
                spwanParent.position += Vector3.forward * spwanDistance;

                SpwanObstacle();
            }

            spwanParent.position -= Vector3.forward * spwanDistance * 2;

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
        bool usingAllLanes = lanesToUse == lanes.Count;

        for (int i = 0; i < lanesToUse; i++)
        {
            ObstacleSpwaner currSpawner = lanes[i].GetComponent<ObstacleSpwaner>();

            if (i == 0)
            {
                currSpawner.SpwanNextObstacleTrigger();
            }

            if (usingAllLanes && i == lanesToUse - 1)
            {
                
                string prevTag = lanes[i - 1].GetComponent<ObstacleSpwaner>().GetLastObstacleTag();
                currSpawner.SpwanDifferentThenLast(prevTag);
                
            }
            else
            {
                currSpawner.Spwan();
            }
        }
    }
}
