using UnityEngine;

public class PlatformSpwanTrigger : MonoBehaviour
{
    Platform platform;
    private void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // When the player enters the trigger, spawn the next platform
        //if (other.CompareTag("Player"))
        //{
        //    //if (platform == null) 
        //    //{
        //    //    platform = gameObject.GetComponentInParent<Platform>();
        //    //}


        //    //platform.SpwanNextPlatform();

            
        //    Debug.Log("Platform Spawn Trigger Hit");
        //}

        // When the destroy platform trigger is hit, destroy the platform
        //if (other.CompareTag("DestroyPlatform")) 
        //{
        //    platform.DestroyPlatform();
        //}
        if (other.CompareTag("DestroyPlatform"))
        {
            platform = gameObject.GetComponentInParent<Platform>();

            platform.DeactivatePlatform();
            PlatformManager.Instance.SpawnNextPlatform();
        }
    }
}
