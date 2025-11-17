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

    // Tank
    [SerializeField] int damagetank;

    // Slide
    bool isSliding;
    [SerializeField] Vector3 slideDirection;
    [SerializeField] float slideSpeed;
    [SerializeField] float slideDuration;



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

            if (type == TrapTypes.puddle || type == TrapTypes.wires)
            {
                NewMonoBehaviourScript player = other.GetComponent<NewMonoBehaviourScript>();
                //Debug.Log("Player");

                if (player != null)
                {
                    //Debug.Log("Player checked");
                    if (type == TrapTypes.puddle)
                    {
                        //Debug.Log("Puddle checked");

                        slideDirection = player.GetMoveDir();

                        player.StartSlide(slideDirection,slideDuration,slideSpeed);
                    }



                }
            }

            Destroy(gameObject);
        }
     }

}
