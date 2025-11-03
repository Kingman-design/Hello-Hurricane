using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAi : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] int gravity;
    [SerializeField] int jumpspeed;
    [SerializeField] int speed;
    [SerializeField] int sidestepSpeed;
    [SerializeField] Rigidbody rb;
    //[SerializeField] NavMeshAgent agent;

    bool ObstacleTrigger;
    bool JumpTrigger;
    bool DuckTrigger;
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

        if(JumpTrigger)
        {
            DodgeJump();
        }
        else if (ObstacleTrigger)
        {
            DodgeMove();
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
            JumpTrigger = true;
        }
        else if (other.CompareTag("Dodge-MGTest"))
        {
            ObstacleTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JumpMG"))
        {
            JumpTrigger = false;
        }
        else if (other.CompareTag("Dodge-MGTest"))
        {
            StartCoroutine(Sidestep());
            ObstacleTrigger = false;
        }
    }

    IEnumerator Sidestep()
    {
        yield return new WaitForSeconds(2.00f);
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

    void DodgeMove()
    {
        rb.linearVelocity = new Vector3(sidestepSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
    }

}
