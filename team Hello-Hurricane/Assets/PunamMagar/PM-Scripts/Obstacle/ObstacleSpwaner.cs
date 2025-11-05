using UnityEngine;

public class ObstacleSpwaner : MonoBehaviour
{
    ObjectPoolerManager objectPoolerManager;
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
        string randTag = objectPoolerManager.GetRandomObstacleTag();
        GameObject obstacle = objectPoolerManager.SpawnFromPool(randTag, transform.position, Quaternion.identity);
        obstacle.GetComponentInParent<Transform>().parent = transform;
        obstacle.GetComponent<Obstacle>().tagName = randTag;
    }
}
