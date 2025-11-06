using UnityEngine;

public class ObstacleSpwaner : MonoBehaviour
{
    ObjectPoolerManager objectPoolerManager;

    string lastObstacleTag = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPoolerManager = ObjectPoolerManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spwan()
    {
        string tag = objectPoolerManager.GetRandomObstacleTag(); ;
        
        GameObject obstacle = objectPoolerManager.SpawnFromPool(tag, transform.position, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().tagName = tag;
        lastObstacleTag = tag;
    }

    public void SpwanDifferentThenLast(string _lastTag) 
    {
        string tag;

        do
        {
            tag = objectPoolerManager.GetRandomObstacleTag();

        } while (tag != _lastTag);

        GameObject obstacle = objectPoolerManager.SpawnFromPool(tag, transform.position, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().tagName = tag;
        lastObstacleTag = tag;
    }

    public string GetLastObstacleTag()
    {
        return lastObstacleTag;
    }
}
