using UnityEngine;

public class Traps : MonoBehaviour
{
    enum TrapTypes
    {
        propane,
        puddle,
        wires
    }

    [SerializeField] TrapTypes type;

    //Tank
    [SerializeField] int damagetank;
    
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

        if (dmg != null && other.CompareTag("Player"))
        {
            if (type == TrapTypes.propane)
            {
                dmg.takeDamage(damagetank);
            }



        }
    }



}
