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
    
    float OrigSize;


    bool ObstacleTrigger;
    bool JumpTrigger;
    bool DuckTrigger;
    Vector3 EnemyVel;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EnemyVel = transform.localScale;

        Debug.Log(EnemyVel);
       
        OrigSize = EnemyVel.y;
        
        Debug.Log(OrigSize);
        
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
        else if (DuckTrigger)
        {
            DodgeDuck();
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
        else if (other.CompareTag("Duck-MGTest"))
        {
            DuckTrigger = true;
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
        else if (other.CompareTag("Duck-MGTest"))
        {
            StartCoroutine(Uncrouch());
            DuckTrigger = false;
        }
    }

    IEnumerator Uncrouch()
    {
        Debug.Log("uncrouch start");
        yield return new WaitForSeconds(1.0f);
        Vector3 scale = transform.localScale;
        scale.y = OrigSize;
        transform.localScale = scale;
        Debug.Log("uncrouch end");
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

    void DodgeDuck()
    {
        EnemyVel = new Vector3(EnemyVel.x, 0.5f, EnemyVel.z);
        transform.localScale = EnemyVel;
        transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

}
