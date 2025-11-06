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

    


}
