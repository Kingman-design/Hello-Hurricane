using UnityEngine;

public class PowerUpObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] PowerUpBase.powerType Type;
    [SerializeField] float duration;
    


   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUpBase.instance.PowerUse(other.GetComponent<NewMonoBehaviourScript>(), Type, duration);
            Destroy(gameObject);
        }
    }
}
