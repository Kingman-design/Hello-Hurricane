using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            Destroy(transform.parent.gameObject);
            dmg.takeDamage(damageAmount);
        }
    }
}
