using UnityEngine;

public class Traps : MonoBehaviour
{
    
    enum traptypes
    {
        propane,
        puddle,
        wires
    }

    [SerializeField] traptypes type;

    // Tank
    [SerializeField] int damagetank;

    // Slide
    bool isSliding;
    [SerializeField] Vector3 slideDirection;
    [SerializeField] float slideSpeed;
    [SerializeField] float slideDuration;

    // Wires
    bool isElectrized;
    [SerializeField] float wireDuration;
    [SerializeField] float wireInterval;
    [SerializeField] float FreezeTime;

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
            if(type == traptypes.propane)
            {
                dmg.takeDamage(damagetank);
            }

            if (type == traptypes.puddle || type == traptypes.wires)
            {
                NewMonoBehaviourScript player = other.GetComponent<NewMonoBehaviourScript>();

                if (player != null && type == traptypes.puddle) // slide
                {
                    //Debug.Log("Player slides start");
                    player.StartSlide(slideDirection, slideDuration, slideSpeed);
                    //Debug.Log("Player slides ends");
                }
                else if (type == traptypes.wires && player != null)
                {
                    //Debug.Log("Wires start");
                    player.StartWires(wireDuration,wireInterval,FreezeTime);
                    //Debug.Log("Wire ends");
                }


            }
            if (type != traptypes.puddle)
            {
                Destroy(gameObject);
            }
        }
    }

}
