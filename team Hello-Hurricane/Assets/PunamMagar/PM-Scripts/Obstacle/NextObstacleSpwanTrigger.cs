using System.Runtime.Serialization;
using UnityEngine;

public class NextObstacleSpwanTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Speed Settings")]
    float speed;

    PlatformManager platformManager;
    private void Start()
    {
        platformManager = PlatformManager.Instance;
    }

    void Update()
    {
        speed = platformManager.GetSpeed();
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyObstacle"))
        {
            ObjectPoolerManager objectPoolerManager = ObjectPoolerManager.Instance;
            string tag = objectPoolerManager.GetNextObstacleTriggerTag();
            objectPoolerManager.ReturnToPool(tag, gameObject);

            ObstacleManager.Instance.SpwanObstacle();
        }
    }

    

    private void Movement()
    {
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);
    }
}
