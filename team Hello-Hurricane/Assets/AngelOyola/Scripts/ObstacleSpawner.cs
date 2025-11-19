using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] float safeZoneDistance = 30f;

    public GameObject[] blockObstaclePrefabs;
    public GameObject[] jumpObstaclePrefabs;
    public GameObject[] rollObstaclePrefabs;

    public Transform[] spawnRows;

    public int blockSpawnChance = 100;
    public float blockObstacleProbability = 0.5f;
    private int lastSafeLane = -1;

    void Start()
    {
        if (transform.position.z < safeZoneDistance) return;

        if (!IsConfigurationValid())
        {
            enabled = false;
            return;
        }

        for (int i = 0; i < spawnRows.Length; i++)
        {
            Transform row = spawnRows[i];

            if (row.position.z < safeZoneDistance) continue;

            List<Transform> pointsInRow = new List<Transform>();
            for (int j = 0; j < row.childCount; j++)
            {
                pointsInRow.Add(row.GetChild(j));
            }

            float randomObstacleRoll = Random.value;

            if (randomObstacleRoll < blockObstacleProbability)
            {
                SpawnBlockObstacles(pointsInRow, blockObstaclePrefabs);
            }
            else
            {
                SpawnMixedObstacles(pointsInRow);
            }
        }
    }

    private bool IsConfigurationValid()
    {
        if (spawnRows.Length == 0) return false;
        if (blockObstaclePrefabs.Length == 0) return false;
        return true;
    }

    private void SpawnMixedObstacles(List<Transform> points)
    {
        bool canJump = jumpObstaclePrefabs.Length > 0;
        bool canRoll = rollObstaclePrefabs.Length > 0;

        if (!canJump && !canRoll)
        {
            SpawnBlockObstacles(points, blockObstaclePrefabs);
            return;
        }

        int specialLane = Random.Range(0, points.Count);
        lastSafeLane = -1;

        for (int i = 0; i < points.Count; i++)
        {
            if (i == specialLane)
            {
                bool spawnJump = Random.value > 0.5f;
                if (spawnJump && canJump)
                    SpawnObstacleAtPoint(points[i], jumpObstaclePrefabs);
                else if (canRoll)
                    SpawnObstacleAtPoint(points[i], rollObstaclePrefabs);
                else
                    SpawnObstacleAtPoint(points[i], jumpObstaclePrefabs);
            }
            else
            {
                SpawnObstacleAtPoint(points[i], blockObstaclePrefabs);
            }
        }
    }

    private void SpawnBlockObstacles(List<Transform> points, GameObject[] prefabsToUse)
    {
        if (prefabsToUse.Length == 0) return;

        int laneToSkip;

        int attempts = 0;
        do
        {
            laneToSkip = Random.Range(0, points.Count);
            attempts++;
        }
        while (laneToSkip == lastSafeLane && attempts < 10);

        lastSafeLane = laneToSkip;

        for (int i = 0; i < points.Count; i++)
        {
            if (i == laneToSkip) continue;

            if (Random.Range(0, 100) < blockSpawnChance)
            {
                SpawnObstacleAtPoint(points[i], prefabsToUse);
            }
        }
    }

    private void SpawnObstacleAtPoint(Transform point, GameObject[] prefabsToUse)
    {
        GameObject randomObstacle = prefabsToUse[Random.Range(0, prefabsToUse.Length)];
        Instantiate(randomObstacle, point.position, point.rotation, point);
    }
}