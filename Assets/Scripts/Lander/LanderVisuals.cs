using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private GameObject landerExplosionVFX;

    [SerializeField] private Material landerMat;

    Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();

        //Set the paerticle system to be disabled at the start
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }

    // Subscribe to the lander events
    private void OnEnable()
    {
        if (lander != null)
        {
            lander.OnUpForce += Lander_OnUpForce;
            lander.OnLeftForce += Lander_OnLeftForce;
            lander.OnRightForce += Lander_OnRightForce;
            lander.OnBeforeForce += Lander_OnBeforeForce;
        }   
    }

    // Unsubscribe from the lander events
    private void OnDisable()
    {
        if (lander != null)
        {
            lander.OnUpForce -= Lander_OnUpForce;
            lander.OnLeftForce -= Lander_OnLeftForce;
            lander.OnRightForce -= Lander_OnRightForce;
            lander.OnBeforeForce -= Lander_OnBeforeForce;
        }
    }

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStarted += Lander_OnStarted;
        //Activate shake
        landerMat.EnableKeyword("SHAKEUV_ON");
        landerMat.DisableKeyword("HOLOGRAM_ON");
    }

    private void Lander_OnStarted(object sender, System.EventArgs e)
    {
        //Deactivate shake 
        landerMat.DisableKeyword("SHAKEUV_ON");
    }

    private void Lander_OnLanded(object sender, Lander.LandedEventArgs e)
    {
        switch(e.landingType)
        {
            case Lander.LandingType.Success:
                landerMat.EnableKeyword("HOLOGRAM_ON");
                break;
            case Lander.LandingType.TooFastLanding:
            case Lander.LandingType.TooSteepAngle:
            case Lander.LandingType.WrongLandingArea:
                if (landerExplosionVFX != null)
                {
                    Instantiate(landerExplosionVFX, transform.position, Quaternion.identity);
                    gameObject.SetActive(false);
                }
                break;
        }
            
    }

    // Event handlers for the lander events
    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }

    // Enable the appropriate thruster particle systems based on the forces applied
    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
    }

    // Helper method to enable or disable a particle system
    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        if (particleSystem == null)
            return;

        var emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }

  
}
