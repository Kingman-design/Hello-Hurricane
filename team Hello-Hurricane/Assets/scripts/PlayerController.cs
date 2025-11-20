using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class NewMonoBehaviourScript : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;
    [SerializeField] new CapsuleCollider collider;

    [SerializeField] int speed;
    [SerializeField] int JumpSpeed;
    [SerializeField] int maxJumps;
    [SerializeField] int gravity;
    [SerializeField] int HP;
    [SerializeField] float targettime;
    [SerializeField] Renderer model;
    [SerializeField] Animator anim;

    [SerializeField] Vector3 minBound, maxBound;

    Vector3 moveDir;
    Vector3 playerVel;

    int jumpCount;
    float timer = 0;
    int oldgravity;
    float oldheight;
    float oldcolliderheight;
    Renderer modelstart;

    // Slide
    bool isSliding;
    //Vector3 slideDirection;
    //float slideSpeed;
    //float slideDuration;

    // Wires
    bool isElectric;
    bool canmove;
    int randomElectric;
    float wireDuration;
    float wireInterval;
    float freezeTime;
    float wireIntervalTimer;
    float wireTotalTime;

    // Audio
    [Header("-----Player Audio-----")]

    [SerializeField] AudioSource aud;

    [SerializeField] AudioClip[] audPuddleSplash;
    [SerializeField][Range(0, 1)] float audPuddleVol;
    [SerializeField] AudioClip[] audPropaneExplosion;
    [SerializeField][Range(0, 1)] float audPropaneVol;
    [SerializeField] AudioClip[] audElectricWires;
    [SerializeField][Range(0, 1)] float audWiresVol;
    [SerializeField] AudioClip[] audBG;
    [SerializeField][Range(0, 1)] float audBGVol;
    [SerializeField] AudioClip[] audJump;
    [SerializeField][Range(0, 1)] float audJumpVol;
    [SerializeField] AudioClip[] audRun;
    [SerializeField][Range(0, 1)] float audRunVol;
    [SerializeField] AudioClip[] audSlide;
    [SerializeField][Range(0, 1)] float audSlideVol;
    [SerializeField] AudioClip[] audCollision;
    [SerializeField][Range(0, 1)] float audCollisionVol;
    [SerializeField] AudioClip[] audPowerUp;
    [SerializeField][Range(0, 1)] float audPowerUpVol;
    [SerializeField] AudioClip[] audDeath;
    [SerializeField][Range(0, 1)] float audDeathVol;

    bool bgPlaying;

    public AudioSource audioSource;
    public AudioClip pickupPowerupSfx;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldgravity = gravity;
        canmove = true;
        oldheight = controller.height;
        oldcolliderheight = collider.height;
        modelstart = model;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x,minBound.x,maxBound.x);

        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.height <= 1)
        {
            gravity = 1000;
            timer += Time.deltaTime;
            if (timer >= targettime)
            {
                controller.height = oldheight;
                timer = 0;
                gravity = oldgravity;
                collider.height = oldcolliderheight;
            }
        }
        movement();

        //background environment audio

        if (!bgPlaying)
        {
            aud.loop = true;
            aud.clip = audBG[Random.Range(0, audBG.Length)];
            aud.volume = audBGVol;
            aud.Play();
            bgPlaying = true;
        }


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

        if (isSliding)
        {
            // slide sound
            if (!aud.isPlaying)
                aud.PlayOneShot(audSlide[Random.Range(0, audSlide.Length)], audSlideVol);

            //if (slideDuration <= 0)
            //{
            //    isSliding = false;
            //}
            //controller.Move(slideDirection * slideSpeed * Time.deltaTime);
            //slideDuration -= Time.deltaTime;

            if (isElectric)
            {
                takeDamage(1);
                isElectric = false;
                wireIntervalTimer = 0;
                wireTotalTime = 0;
            }

        }
        else if (isElectric)
        {
            Electrified();

            if (randomElectric == 0)
            {
                StartCoroutine(freezeplayer());
            }

        }

        if (canmove)
        {
            moveDir = horizontal * transform.right + vertical * transform.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
            //run audio
            if (controller.isGrounded && moveDir.magnitude > 0.1f && !aud.isPlaying)
            {
                aud.PlayOneShot(audRun[Random.Range(0, audRun.Length)], audRunVol);
            }


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
            anim.SetTrigger("Jump");

            //jump audio
            aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
        }
    }
    void crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            controller.height = controller.height / 2;
            collider.height = controller.height / 2;
            model.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y * 2, transform.position.z);
            anim.SetTrigger("Crouch");
        }
    }

    public void takeDamage(int amount)
    {
        //collision to walls audio
        aud.PlayOneShot(audCollision[Random.Range(0, audCollision.Length)], audCollisionVol);
        HP -= amount;

        if (HP <= 0)
        {
            // dead audio
            aud.PlayOneShot(audDeath[Random.Range(0, audDeath.Length)], audDeathVol);

            UIManager.instance.stateLose();
        }
    }

    //public void StartSlide(Vector3 dir, float duration, float speed)
    //{
    //    isSliding = true;
    //    slideDuration = duration;
    //    slideSpeed = speed;
    //    slideDirection = dir;
    //}

    public void IsOnPuddle()
    {
        isSliding = true;

        //puddle audio
        aud.PlayOneShot(audPuddleSplash[Random.Range(0, audPuddleSplash.Length)], audPuddleVol);

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
        // wire audio
        aud.PlayOneShot(audElectricWires[Random.Range(0, audElectricWires.Length)], audWiresVol);
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

    //audio trigger call

    public void PlayPowerUpSound()
    {
        if (audioSource != null && pickupPowerupSfx != null)
        {
            audioSource.PlayOneShot(pickupPowerupSfx);
        }
    }
}
