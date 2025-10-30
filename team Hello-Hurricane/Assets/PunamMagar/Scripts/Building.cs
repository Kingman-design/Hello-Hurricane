using UnityEngine;

public class Building : MonoBehaviour
{
    float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = PlatformManager.Instance.GetPlatformSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement() 
    {
        transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
    }

    
}
