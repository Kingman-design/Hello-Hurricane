using System;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioSource bgEnviroAudio;
    [SerializeField] AudioSource runAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource landAudio;
    [SerializeField] AudioSource duckAudio;
    [SerializeField] CharacterController controller;

    [SerializeField] float pitchRate = 0.05f;
    [SerializeField] float maxPitch = 2.5f;
    [SerializeField] float pitchSpeedUp = 0.2f;

    bool wasGroundLastF; 
    float bAudioPitch;
    float bRunPitch;

    void Start()
    {
        bAudioPitch = bgEnviroAudio.pitch;  //starting pitch for the loops
        bRunPitch = runAudio.pitch;

        if(bgEnviroAudio != null && !bgEnviroAudio.isPlaying) // plays bg loop
        {
            bgEnviroAudio.Play();

            wasGroundLastF = controller.isGrounded; 
        } 
    }

    void Update()
    {
        hdlrRun();
        hdlrJump();
        hdlrLand();
        hdlrDuck();
        updatePitch();

    }
    void hdlrRun()
    {
        bool moving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        if (moving && controller.isGrounded)
        {
            if (!runAudio.isPlaying)
            {
                runAudio.loop = true;
                runAudio.Play();
            }
        }
        else
        {
            if (runAudio.isPlaying)
                runAudio.Stop();
        }
    }
    private void hdlrJump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            jumpAudio.Play();
        }
    }
    private void hdlrLand()
    {
        if (!wasGroundLastF && controller.isGrounded)
        {
            landAudio.Play();
        }

        wasGroundLastF = controller.isGrounded;
    }
    private void hdlrDuck()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            duckAudio.Play();
        }
    }

    private void updatePitch()
    {
        float targetPitch = Mathf.Clamp(bAudioPitch + (/*playerSpeed * */ 0.02f), bAudioPitch, maxPitch);
    }

    // note: bg music needs loop and play on awake, run audio is looped, jump/land/duck have neither.
    // note: adjust pitch as needed
    // ps: revision needed







}
