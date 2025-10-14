using System;
using UnityEngine;
using static ForcesSO;

public class ForceZone : MonoBehaviour, IInteractableStay, IInteractableExit
{
    [SerializeField] private ForcesSO forcesSO;

    [Header("Directional Force")]
    [SerializeField] private float directionMagnitude;

    [Header("Radial Force")]
    [SerializeField] private float radialMagnitude;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private AnimationCurve radialFallOff;
    [SerializeField] private float radialRadius = 5f;

    [Header("Radial Spring-Damper")]
    [SerializeField] private float springK = 30f;       
    [SerializeField] private float dampingRadial = 7f;  
    [SerializeField] private float dampingTangential = 3f; 
    [SerializeField] private float maxForce = 60f;     

    [Header("Capture")]
    [SerializeField] private float captureRadius = 0.15f;   
    [SerializeField] private float captureDamping = 12f;    

    [Header("Anti-Gravity (opcional)")]
    [SerializeField] private bool cancelGlobalGravity = true; 
    [SerializeField, Range(0f, 1.5f)] private float antiGravityFactor = 1f; 


    [SerializeField] private float gravity;

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

    // Apply a radial foorce + spring-damper to the Rigidbody2D
    private void ApplyRadial(Rigidbody2D rb)
    {
        if (centerPoint == null) return;

        Vector2 toCenter = (Vector2)centerPoint.position - rb.position;//To the center
        float dist = toCenter.magnitude;
        if (dist > radialRadius) return;

        // Nomralized direction to center (or up if very close)
        Vector2 n = dist > 1e-4f ? toCenter / dist : Vector2.up;
        Vector2 v = rb.linearVelocity;

        // --- Falloff ---
        float t = Mathf.Clamp01(dist / radialRadius);
        float fall = radialFallOff != null ? Mathf.Clamp01(radialFallOff.Evaluate(t)) : 1f;

        // --- Hooke's law (F = -k*x) ---
        Vector2 Fspring = n * (springK * dist * fall);

        // --- Radial damping ---   
        float vRad = Vector2.Dot(v, n);
        Vector2 FradDamp = -vRad * n * dampingRadial;

        // --- Tangential damping ---
        Vector2 vTan = v - vRad * n;                
        Vector2 FtanDamp = -vTan * dampingTangential;

        // --- Anti-gravity --- (Opcional)  
        Vector2 FantiG = Vector2.zero;
        if (cancelGlobalGravity)
        {
           
            Vector2 g = Physics2D.gravity * rb.gravityScale; 
            FantiG = -g * rb.mass * antiGravityFactor;
        }

        // --- Extra damping ---
        Vector2 Fcapture = Vector2.zero;
        if (dist < captureRadius)
        {
            Fcapture = -v * captureDamping; 
        }

        // --- Total force ---
        Vector2 F = Fspring + FradDamp + FtanDamp + FantiG + Fcapture;
        if (F.sqrMagnitude > maxForce * maxForce)
            F = F.normalized * maxForce;

        rb.AddForce(F, ForceMode2D.Force);
    }


    private void OnDrawGizmos()
    {
        if (centerPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(centerPoint.position, radialRadius);
        }
    }

    // Apply a gravity force to the Rigidbody2D
    private void ApplyGravity(Rigidbody2D rb)
    {
        Vector2 currentG = Physics2D.gravity * rb.gravityScale;
        Vector2 desiredG = Physics2D.gravity * gravity;

        Vector2 gravityTotal = desiredG - currentG;
        rb.AddForce(gravityTotal * rb.mass, ForceMode2D.Force);

        Lander.Instance.force = 200f;
    }

    private void ApplyImpulse(Rigidbody2D rb)
    {
        throw new NotImplementedException();
    }

    private void ApplyTorque(Rigidbody2D rb)
    {
        //Quaternion forceRotation = forcesSO.torqueDirection;
    }

    private void ApplyDirection(Rigidbody2D rb)
    {
        Vector2 forceDirection = forcesSO.direction;

        switch (forcesSO.forceDirection)
        {
            case ForcesSO.ForceDirection.up:
                forceDirection = Vector2.up * directionMagnitude;
                break;
            case ForcesSO.ForceDirection.down:
                forceDirection = Vector2.down * directionMagnitude;
                break;
            case ForcesSO.ForceDirection.left:
                forceDirection = Vector2.left * directionMagnitude;
                break;
            case ForcesSO.ForceDirection.right:
                forceDirection = Vector2.right * directionMagnitude;
                break;
        }

        rb.AddForce(forceDirection, ForceMode2D.Force);
    }

    public void Exit(Lander lander)
    {
        Lander.Instance.force = 700f;
    }
}
