using System.Runtime.Serialization;
using UnityEngine;

public class Building : MonoBehaviour
{
    enum BuildingType 
    {
        Foreground,
        Background
    }

    [Header("Background Building Settings")]
    [SerializeField] BuildingType buildingType = BuildingType.Foreground;

    [Range(0,1)]
    [SerializeField] float backgroundSpeedFactor = 1f; 

    float moveSpeed;

    [HideInInspector]
    public string tagName;

    [Header("")]
    [SerializeField] Renderer buildingRenderer;

    PlatformManager platformManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformManager = PlatformManager.Instance;

        if (buildingType == BuildingType.Foreground) 
        {
            backgroundSpeedFactor = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = platformManager.GetSpeed() * backgroundSpeedFactor;
        Movement();
    }

    private void LateUpdate()
    {
        
    }

    void Movement() 
    {
        transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
    }

    public void ReturnBuilding() 
    {
        ObjectPoolerManager.Instance.ReturnToPool(tagName, gameObject);
    }

    public Renderer GetBuildingRenderer() 
    {
        return buildingRenderer;
    }
}
