using System.Collections;
using System.Data;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;

    [SerializeField] int speed;
    [SerializeField] int JumpSpeed;
    [SerializeField] int maxJumps;
    [SerializeField] int gravity;
    [SerializeField] int HP;
    [SerializeField] float targettime;
    Vector3 moveDir;
    Vector3 playerVel;

    int jumpCount;
    float timer = 0;
    int oldgravity = 0;

    // Slide
    bool isSliding;
    Vector3 slideDirection;
    float slideSpeed;
    float slideDuration;

    // Wires
    bool isElectric;
    bool canmove;
    int randomElectric;
    float wireDuration;
    float wireInterval;
    float freezeTime;
    float wireIntervalTimer;
    float wireTotalTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldgravity = gravity;
        canmove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y == 0.5f)
        {
            if (gravity < 50)
            {
               oldgravity = gravity;
            }
            gravity = 1000;
            timer += Time.deltaTime;
            if (timer >= targettime)
            {
                transform.localScale = new Vector3(1, 1, 1);
                timer = 0;
                gravity = oldgravity;
            }
        }
        movement();  
    }

    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            playerVel = Vector3.zero;
            jumpCount = 0;
        }
        else
        {
            playerVel.y -= gravity * Time.deltaTime;
        }

        if(isSliding)
        {
            if(slideDuration <= 0)
            {
                isSliding = false;
            }


            controller.Move(slideDirection * slideSpeed * Time.deltaTime);
            slideDuration -= Time.deltaTime;

        }
        else if (isElectric)
        {
            Electrified();

            if(randomElectric == 0)
            {
                StartCoroutine(freezeplayer());
            }

        }

        if (canmove)
        {
            moveDir = horizontal * transform.right + vertical * transform.forward;
            controller.Move(moveDir * speed * Time.deltaTime); 
            jump();
            crouch();
            controller.Move(playerVel * Time.deltaTime);
        }
       
    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            playerVel.y = JumpSpeed;
            jumpCount++;
        }
    }
    void crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y - 1, transform.localPosition.z);
        }
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            UIManager.instance.stateLose();
        }
    }

    public void StartSlide(Vector3 dir, float duration, float speed)
    {
        isSliding = true;
        slideDuration = duration;
        slideSpeed = speed;
        slideDirection = dir;
    }

    public void StartWires(float duration, float interval, float freezetime)
    {
        isElectric = true;
        wireInterval = interval;
        wireDuration = duration;
        freezeTime = freezetime;
        wireTotalTime = 0;
        wireIntervalTimer = 0;
    }

    private void Electrified()
    {
        wireIntervalTimer += Time.deltaTime;
        wireTotalTime += Time.deltaTime;
        while (wireTotalTime <= wireDuration && wireIntervalTimer >= wireInterval)
        {
            randomElectric = Random.Range(0, 5);
            wireIntervalTimer = 0;
        }
        if (wireTotalTime > wireDuration)
        {
            isElectric = false;
            wireIntervalTimer = 0;
            wireTotalTime = 0;
        }
    }

    IEnumerator freezeplayer()
    {
        canmove = false;
        yield return new WaitForSeconds(freezeTime);
        canmove = true;
    }    


    public Vector3 GetMoveDir()
    {
        return moveDir;
    }

    public int GetSpeed()
    {
        return speed;
    }
    public int GetJumpSpeed()
    {
        return JumpSpeed;
    }
    public int GetMaxJumps()
    {
        return maxJumps;
    }
    public int GetGravity()
    {
        return gravity;
    }
    public int GetHP()
    {
        return HP;
    }

    public void SetSpeed(int NewSpeed)
    {
        speed = NewSpeed;
    }
    public void SetJumpSpeed(int NewJumpSpeed)
    {
        JumpSpeed = NewJumpSpeed;
    }
    public void SetMaxJumps(int NewMaxJump)
    {
        maxJumps = NewMaxJump;
    }
    public void SetGravity(int NewGravity)
    {
        gravity = NewGravity;
    }
    public void SetHP(int NewHP)
    {
        HP = NewHP;
    }

}
