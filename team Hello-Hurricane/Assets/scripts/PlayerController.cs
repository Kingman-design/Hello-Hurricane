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

    // Sliding
    bool isSliding;
    Vector3 slideDirection;
    float slideSpeed;
    float slideDuration;

    // Wires
    bool isElectric;
    float wireDuration;
    float wireInterval;
    float wirecounter;
    float wiretimer = 0f;
    float Freezetime;
    int randomElectric;

    bool canmove;
    int jumpCount;
    float timer = 0f;
    int oldgravity;

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
            gravity = 1000;
            timer += Time.deltaTime;
            if (timer >= targettime)
            {
                transform.localScale = new Vector3(1, 1, 1);
                //controller.height = 2;
                timer = 0;
                gravity = oldgravity;
            }
        }
        movement();
    }

    void movement()
    {
        if (controller.isGrounded)
        {
            playerVel = Vector3.zero;
            jumpCount = 0;
        }
        else
        {
            playerVel.y -= gravity * Time.deltaTime;
        }

        if (isSliding)
        {
            if (slideDuration <= 0)
            {
                isSliding = false;
            }
            
            controller.Move(slideDirection * slideSpeed * Time.deltaTime);
            slideDuration -= Time.deltaTime;

        }
        else if (isElectric)
        {
            Electrified();
            wiretimer += Time.deltaTime;
            wirecounter += Time.deltaTime;

            if (randomElectric == 0)
            { 
                Debug.Log("Zapped");
                StartCoroutine(freezeplayer());
            }
        }

        if (canmove)
        { 
            moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        jump();
        crouch();
        controller.Move(playerVel * Time.deltaTime);
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
            //controller.height = 1;
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
        slideDirection = dir.normalized;
        slideDuration = duration;
        slideSpeed = speed;
    }

    public void StartWires(float duration, float interval, float freezetime)
    {
        //Debug.Log(wireInterval);
        isElectric = true;
        wireInterval = interval;
        wireDuration = duration;
        Freezetime = freezetime;
        wirecounter = 0;
        //Debug.Log(wireInterval);
    }

    private void Electrified()
    {

        //Debug.Log("Interval start");
        while(wiretimer <= wireDuration && wirecounter >= wireInterval)
        {
            Debug.Log("Start timer");
            randomElectric = Random.Range(0, 5);
            wirecounter = 0;
        }
        if (wiretimer > wireDuration)
        {
            isElectric = false;
            wiretimer = 0f;
        }
    }

    IEnumerator freezeplayer()
    {
        canmove = false;
        yield return new WaitForSeconds(Freezetime);
        canmove = true;
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
