using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [System.Serializable]
    public struct MinMax 
    {
        public float min;
        public float max;
    }

    [Header("Spwan Settings")]
    [SerializeField] private List<GameObject> lanes;

    [SerializeField] MinMax spawnInterval;
    private float spwanTimer = 0f;
    private float randSpwanTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randSpwanTime = Random.Range(spawnInterval.min, spawnInterval.max);
    }

    // Update is called once per frame
    void Update()
    {
        spwanTimer += Time.deltaTime;

        if (spwanTimer >= randSpwanTime) 
        {
            SpwanObstacle();
            spwanTimer = 0f;
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

    void SpwanObstacle()
    {
        ShuffleLanes(lanes);

        int lanesToUse = Random.Range(1, lanes.Count + 1);

        for (int i = 0; i < lanesToUse; i++)
        {
            lanes[i].GetComponent<ObstacleSpwaner>().Spwan();
        }

        randSpwanTime = Random.Range(spawnInterval.min, spawnInterval.max);
    }
}
