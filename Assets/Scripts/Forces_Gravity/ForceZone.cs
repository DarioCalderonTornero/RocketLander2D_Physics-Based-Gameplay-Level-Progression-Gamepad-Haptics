using UnityEngine;

public class ForceZone : MonoBehaviour, IInteractableStay
{
    [SerializeField] private ForcesSO forcesSO;

    public void Stay(Lander lander)
    {
        if (forcesSO == null)
            return;

        Vector2 forceDirection = Vector2.zero;

        if (forcesSO.forceType == ForcesSO.ForceType.Directional)
        {
            switch(forcesSO.forceDirection)
            {
                case ForcesSO.ForceDirection.up:
                    forceDirection = Vector2.up * forcesSO.magnitude;
                    break;
                case ForcesSO.ForceDirection.down:
                    forceDirection = Vector2.down * forcesSO.magnitude;
                    break;
                case ForcesSO.ForceDirection.left:
                    forceDirection = Vector2.left * forcesSO.magnitude;
                    break;
                case ForcesSO.ForceDirection.right:
                    forceDirection = Vector2.right * forcesSO.magnitude;
                    break;
            }


            lander.GetComponent<Rigidbody2D>().AddForce(forceDirection, ForceMode2D.Force);  
        }
    }
}
