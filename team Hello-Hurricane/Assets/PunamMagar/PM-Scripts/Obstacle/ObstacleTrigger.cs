using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleTrigger : MonoBehaviour
{
    [SerializeField] Obstacle obstacle;
    void OnTriggerEnter(Collider other)
    {
        // When the building enters the trigger, return the obstacle to the pool
        if (other.CompareTag("DestroyObstacle"))
        {
            obstacle.ReturnObstacle();

            //ObstacleManager.Instance.SpwanObstacle();
        }
    }
}
