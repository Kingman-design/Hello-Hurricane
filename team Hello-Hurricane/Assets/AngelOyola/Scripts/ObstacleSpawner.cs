using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] blockObstaclePrefabs; // Default obstacles (just normal walls)
    public GameObject[] jumpObstaclePrefabs; // Obstacles for jumping over
    public GameObject[] rollObstaclePrefabs; // Obstacles for rolling over

    public Transform[] spawnRows;

    public int blockSpawnChance = 100;

    public float blockObstacleProbability = 0.6f;


    void Start()
    {
        if (!IsConfigurationValid())
        {
            Debug.LogError("Obstacle Spawner is not configured. Disabling.", this);
            enabled = false;
            return;
        }

        for (int i = 0; i < spawnRows.Length; i++)
        {
            Transform row = spawnRows[i];

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

    // Helper function
    private bool IsConfigurationValid()
    {
        if (spawnRows.Length == 0) return false;
        if (blockObstaclePrefabs.Length == 0 && jumpObstaclePrefabs.Length == 0 && rollObstaclePrefabs.Length == 0) return false;
        return true;
    }

    // This is to spawn 3 obstacles but different types (for jumping over or rolling under obstacles)
    void SpawnMixedObstacles(List<Transform> points)
    {
        bool canJump = jumpObstaclePrefabs.Length > 0;
        bool canRoll = rollObstaclePrefabs.Length > 0;
        if (blockObstaclePrefabs.Length == 0 || (!canJump && !canRoll))
        {
            SpawnBlockObstacles(points, blockObstaclePrefabs);
            return;
        }

        int specialLane = Random.Range(0, points.Count);

        for (int i = 0; i < points.Count; i++)
        {
            if (i == specialLane)
            {
                bool spawnJump = Random.value > 0.5f;

                if (spawnJump && canJump)
                {
                    SpawnObstacleAtPoint(points[i], jumpObstaclePrefabs);
                }
                else if (canRoll)
                {
                    SpawnObstacleAtPoint(points[i], rollObstaclePrefabs);
                }
                else
                {
                    SpawnObstacleAtPoint(points[i], jumpObstaclePrefabs);
                }
            }
            else
            {
                SpawnObstacleAtPoint(points[i], blockObstaclePrefabs);
            }
        }
    }

    // Function to spawn only two obstacles instead of 3 (so it doesnt form an impossible wall)
    void SpawnBlockObstacles(List<Transform> points, GameObject[] prefabsToUse)
    {
        if (prefabsToUse.Length == 0) return;

        int laneToSkip = Random.Range(0, points.Count);

        for (int i = 0; i < points.Count; i++)
        {
            if (i == laneToSkip)
            {
                continue;
            }

            if (Random.Range(0, 100) < blockSpawnChance)
            {
                SpawnObstacleAtPoint(points[i], prefabsToUse);
            }
        }
    }

    // Helper function
    void SpawnObstacleAtPoint(Transform point, GameObject[] prefabsToUse)
    {
        GameObject randomObstacle = prefabsToUse[Random.Range(0, prefabsToUse.Length)];
        Instantiate(randomObstacle, point.position, point.rotation, point);
    }
}