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

    public void Spwan(bool _diffObstacle = false, string _lastTag = null)
    {
        string tag;
        if (_diffObstacle == false)
        {
            tag = objectPoolerManager.GetRandomObstacleTag();
        }
        else
        {
            while (true)
            {
                tag = objectPoolerManager.GetRandomObstacleTag();
                if (tag != _lastTag)
                {

                    break;
                }
            }
        }

        GameObject obstacle = objectPoolerManager.SpawnFromPool(tag, transform.position, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().tagName = tag;
        lastObstacleTag = tag;
    }

    public string GetLastObstacleTag()
    {
        return lastObstacleTag;
    }
}
