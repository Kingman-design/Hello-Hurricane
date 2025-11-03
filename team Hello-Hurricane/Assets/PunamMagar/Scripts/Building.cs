using System.Runtime.Serialization;
using UnityEngine;

public class Building : MonoBehaviour
{
    float moveSpeed;

    [HideInInspector]
    public string tagName;

    [SerializeField] Renderer buildingRenderer;

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
