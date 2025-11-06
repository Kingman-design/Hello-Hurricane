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
        incPitchOverTime();

    }


}
