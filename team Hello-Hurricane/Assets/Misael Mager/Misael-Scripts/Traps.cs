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

    [SerializeField] int damagetank;

    bool isSliding;
    [SerializeField] Vector3 slideDirection;
    [SerializeField] float slideSpeed;
    [SerializeField] float slideDuration;


    bool isElectrized;

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

            if (type == traptypes.puddle)
            {
                Debug.Log("slides starts");
                NewMonoBehaviourScript player = other.GetComponent<NewMonoBehaviourScript>();
                if (player != null)
                {
                    Debug.Log("Player slides start");
                    player.StartSlide(slideDirection, slideDuration, slideSpeed);
                    Debug.Log("Player slides ends");
                }

                Debug.Log("slides ends");
            }

            Destroy(gameObject);
        }
    }

}
