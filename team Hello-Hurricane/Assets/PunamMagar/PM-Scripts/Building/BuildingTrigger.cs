using UnityEngine;

public class BuildingTrigger : MonoBehaviour
{
    [SerializeField] Building building;
    

    void OnTriggerEnter(Collider other)
    {
        // When the building enters the trigger, return the building to the pool
        if (other.CompareTag("DestroyBuilding"))
        {
            building.ReturnBuilding();
            building.buildingSpwaner.SpwanBuilding();
        }
    }
}
