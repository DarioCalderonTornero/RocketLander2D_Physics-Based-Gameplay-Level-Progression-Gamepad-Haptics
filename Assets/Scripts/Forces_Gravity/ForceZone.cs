using System;
using UnityEngine;
using static ForcesSO;

public class ForceZone : MonoBehaviour, IInteractableStay
{
    [SerializeField] private ForcesSO forcesSO;
    [SerializeField] private float magnitude;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private AnimationCurve falloff;

    public void Stay(Lander lander)
    {
        if (forcesSO == null)
            return;

        Rigidbody2D rb = lander.GetComponent<Rigidbody2D>();    

        if (rb == null)
        {
            return;
        }

        switch(forcesSO.forceType)
        {
            case ForcesSO.ForceType.Directional:
                ApplyDirection(rb);
                break;
            case ForcesSO.ForceType.radial:
                ApplyRadial(rb);
                break;
            case ForcesSO.ForceType.gravity:
                ApplyGravity(rb);
                break;
            case ForcesSO.ForceType.impulse:
                ApplyImpulse(rb);
                break;
            case ForcesSO.ForceType.Torque:
                ApplyTorque(rb);
                break;
        }
    }

    private void ApplyRadial(Rigidbody2D rb)
    {
        if (centerPoint == null)
            return;

        Vector2 direction = rb.position - (Vector2)centerPoint.position;
        float distance = direction.magnitude;

        if (distance > forcesSO.radius)
            return;

        float fallOfFactor = falloff.Evaluate(distance / forcesSO.radius);
        //fallOfFactor = Mathf.Max(fallOfFactor, 0.05f);
        direction.Normalize();

        Vector2 force = direction * magnitude * fallOfFactor;
        if (forcesSO.isAttractive) force = -force;

        rb.AddForce(force, ForceMode2D.Force);
    }

    private void OnDrawGizmos()
    {
        if (centerPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(centerPoint.position, forcesSO.radius);
        }
    }


    private void ApplyGravity(Rigidbody2D rb)
    {
        throw new NotImplementedException();
    }

    private void ApplyImpulse(Rigidbody2D rb)
    {
        throw new NotImplementedException();
    }

    private void ApplyTorque(Rigidbody2D rb)
    {
        throw new NotImplementedException();
    }

    private void ApplyDirection(Rigidbody2D rb)
    {
        Vector2 forceDirection = forcesSO.direction;

        switch (forcesSO.forceDirection)
        {
            case ForcesSO.ForceDirection.up:
                forceDirection = Vector2.up * magnitude;
                break;
            case ForcesSO.ForceDirection.down:
                forceDirection = Vector2.down * magnitude;
                break;
            case ForcesSO.ForceDirection.left:
                forceDirection = Vector2.left * magnitude;
                break;
            case ForcesSO.ForceDirection.right:
                forceDirection = Vector2.right * magnitude;
                break;
        }

        rb.AddForce(forceDirection, ForceMode2D.Force);
    }

}
