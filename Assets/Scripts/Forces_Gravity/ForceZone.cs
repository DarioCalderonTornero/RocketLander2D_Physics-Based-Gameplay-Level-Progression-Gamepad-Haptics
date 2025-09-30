using UnityEngine;

public class ForceZone : MonoBehaviour, IInteractableStay
{
    [SerializeField] private ForcesSO forcesSO;

    public void Stay(Lander lander)
    {
        if (forcesSO == null)
            return;

        Debug.Log("Entering Left Force Zone");

        if (forcesSO.forceType == ForcesSO.ForceType.Directional)
        {
            Vector2 forceDirection = Vector2.left.normalized;

            Vector2 force = forceDirection * forcesSO.magnitude;

            lander.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);  
        }
    }
}
