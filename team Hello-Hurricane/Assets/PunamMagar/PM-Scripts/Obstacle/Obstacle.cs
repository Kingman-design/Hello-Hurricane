using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] float obstacleSpeed;


    [HideInInspector]
    public string tagName;
    PlatformManager platformManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformManager = PlatformManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        obstacleSpeed = platformManager.GetSpeed();
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.forward * -obstacleSpeed * Time.deltaTime);
    }

    public void ReturnObstacle()
    {
        ObjectPoolerManager.Instance.ReturnToPool(tagName, gameObject);
    }
}
