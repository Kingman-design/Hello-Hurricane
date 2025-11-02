using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] int gravity;
    [SerializeField] int jumpspeed;
    [SerializeField] int speed;
    [SerializeField] Rigidbody rb;
    //[SerializeField] NavMeshAgent agent;

    bool ObstacleTrigger;
    Vector3 EnemyVel;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player-MisealTest").GetComponent<Transform>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement();

        if(ObstacleTrigger)
        {
            DodgeJump();
        }
        else
        {
            Vector3 vel = rb.linearVelocity;
            vel.y -= gravity * Time.deltaTime;
            rb.linearVelocity = vel;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpMG"))
        {
            ObstacleTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JumpMG"))
        {
            ObstacleTrigger = false;
        }
    }

    void enemyMovement()
    {
        Vector3 moveDir = (target.position - transform.position).normalized;
        moveDir.y = 0;

        Vector3 moveVel = moveDir * speed;

        rb.linearVelocity = moveVel;
    }

    void DodgeJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpspeed, rb.linearVelocity.z);
    }

}
