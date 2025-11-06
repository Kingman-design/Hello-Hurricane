using UnityEngine;
using System.Collections;

public class damage : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] int damageAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            dmg.takeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
