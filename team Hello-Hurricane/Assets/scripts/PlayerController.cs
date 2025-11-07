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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        if (controller.isGrounded)
        {
            playerVel = Vector3.zero;
            jumpCount = 0;
        }
        else
        {
            playerVel.y -= gravity * Time.deltaTime;
        }

        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);

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
            transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y - 1, transform.localPosition.z);
        }
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            gameManager.instance.stateLose();
        }
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
