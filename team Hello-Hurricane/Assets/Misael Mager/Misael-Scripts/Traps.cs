using UnityEngine;

public class Traps : MonoBehaviour
{

    [SerializeField] int damagetank;

    bool ispuddleactive;
    bool iswiresactive;

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
            if(other.CompareTag("Player"))
            {
                dmg.takeDamage(damagetank);
            }


            Destroy(gameObject);
        }
    }

}
