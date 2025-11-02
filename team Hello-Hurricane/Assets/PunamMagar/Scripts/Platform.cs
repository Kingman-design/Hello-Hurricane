using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Renderer modelRenderer;

    [SerializeField] float moveSpeed = 2f;

    PlatformManager platformManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformManager = PlatformManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = platformManager.GetSpeed();
        Movement();
    }

    void Movement() 
    {
        transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
    }

    public void IncreaseSpeed(float _speedMod) 
    {
        moveSpeed *= _speedMod;
    }

    public void DecreaseSpeed(float _speedMod) 
    {
        moveSpeed /= _speedMod;
    }

    public void SpwanNextPlatform() 
    {
        PlatformManager.Instance.SpawnNextPlatform(GetEndPosition());
    }

    Vector3 GetEndPosition() 
    {
        Bounds modelBound = modelRenderer.bounds;

        float halfLength = modelBound.extents.z;
        Vector3 localEnd = Vector3.forward * halfLength;

        return transform.TransformPoint(localEnd);
    }

    public Renderer GetModelRenderer() 
    {
        return modelRenderer;
    }

    public void DestroyPlatform()
    {
        Destroy(gameObject);
    }
}
