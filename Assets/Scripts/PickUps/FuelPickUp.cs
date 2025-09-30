using UnityEngine;

public class FuelPickUp : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject fuelPickUpVFX; 


    public void Interact(Lander lander)
    {
        lander.GrabFuelPickUp(this);

        if (fuelPickUpVFX != null)
        {
            Instantiate(fuelPickUpVFX, this.gameObject.transform.position, Quaternion.identity);    
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
