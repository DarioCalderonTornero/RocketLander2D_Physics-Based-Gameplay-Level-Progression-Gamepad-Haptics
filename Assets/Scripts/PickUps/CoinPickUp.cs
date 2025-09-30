using UnityEngine;

public class CoinPickUp : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject coinPickUpVFX;

    public void Interact(Lander lander)
    {
        lander.GrabCoinPickUp(this);

        if (coinPickUpVFX != null)
        {
            Instantiate(coinPickUpVFX, this.gameObject.transform.position, Quaternion.identity);
        }


    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
