using UnityEngine;

public class PlatformSpwanTrigger : MonoBehaviour
{
    Platform platform;
    private void Start()
    {
        platform = gameObject.GetComponentInParent<Platform>();
    }

    void OnTriggerEnter(Collider other)
    {
        // When the player enters the trigger, spawn the next platform
        if (other.CompareTag("Player"))
        {
            platform.SpwanNextPlatform();
        }

        // When the destroy platform trigger is hit, destroy the platform
        if (other.CompareTag("DestroyPlatform")) 
        {
            platform.DestroyPlatform();
        }
    }
}
