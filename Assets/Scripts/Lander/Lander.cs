using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    public static Lander Instance { get; private set; }

    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;

    public event EventHandler OnCoinPickUp;
    public event EventHandler OnFuelPickUp;

    public event EventHandler OnLowFuel;
    public event EventHandler OnHighFuel;

    public event EventHandler OnStarted;

    public event EventHandler<StateChangedEventArgs> OnStateChanged;

    public class StateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public event EventHandler<LandedEventArgs> OnLanded;

    public class LandedEventArgs : EventArgs
    {
        public LandingType landingType;
        public int score;
        public float scoreMultiplier;
        public float landingSpeed;
        public float dotVector;
    }

    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanding,
    }

    public enum State
    {
        WaitingToStart,
        Playing,
        GameOver,
    }

    private State state;
    private Rigidbody2D landerRigidBody2D;

    private const float GRAVITY_NORMAL = 0.7f;

    private float fuelAmount;
    private float fuelAmountMax = 10;

    private bool hasTriggeredLowFuel = false; 
    private const float fuelLowAmount = 0.3f;

    // Deadzone configurable
    private float stickDeadzone = 0.3f;

    private void Awake()
    {
        Instance = this;

        fuelAmount = fuelAmountMax;
        state = State.WaitingToStart;


        landerRigidBody2D = GetComponent<Rigidbody2D>();
        landerRigidBody2D.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        // Reset forces each frame
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        Vector2 movementInput = GameInputs.Instance.GetPlayerMovement();
        bool isLeftMovement = GameInputs.Instance.IsLeftActionPressed();
        bool isRightMovement = GameInputs.Instance.IsRightActionPressed();

        switch (state)
        {
            default:
            case State.WaitingToStart:
                // --- Input with deadzone ---

                bool isStarted = GameInputs.Instance.IsUpActionPressed();

                if (isStarted)
                {
                    landerRigidBody2D.gravityScale = GRAVITY_NORMAL;

                    OnStarted?.Invoke(this, EventArgs.Empty);   

                    if (GameInputs.Instance.IsGamePadConnected())
                    {
                        RumbleManager.Instance.RumblePulse(0.5f, 0.5f, 0.4f);
                    }

                    SetState(State.Playing);
                }
                break;

            case State.Playing:

                if (fuelAmount <= 0f)
                {
                    fuelAmount = 0f;
                    return;
                }

                // --- Input with deadzone ---
                bool thrustUp = GameInputs.Instance.IsUpActionPressed();
                bool thrustLeft = movementInput.x < -stickDeadzone || isLeftMovement;
                bool thrustRight = movementInput.x > stickDeadzone || isRightMovement;

                if (thrustUp || thrustLeft || thrustRight)
                {
                    ConsumeFuel();
                    Debug.Log("ConsumingFuel");
                }

                // UP
                if (thrustUp)
                {
                    float force = 700f;
                    landerRigidBody2D.AddForce(force * transform.up * Time.deltaTime);
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }

                // LEFT
                if (thrustLeft || isLeftMovement)
                {
                    float turnSpeed = 100f;
                    landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }

                // RIGHT
                if (thrustRight || isRightMovement)
                {
                    float turnSpeed = -100f;
                    landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                // Do nothing
                break;

        }
    }

    // ---------------- LANDING LOGIC ----------------
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.TryGetComponent(out Rope rope))
        {
            return;
        }

        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Crashed!");
            OnLanded?.Invoke(this, new LandedEventArgs
            {
                landingType = LandingType.WrongLandingArea,
                dotVector = 0f,
                landingSpeed = 0f,
                scoreMultiplier = 0f,
                score = 0,
            });

            if (GameInputs.Instance.IsGamePadConnected())
            {
                RumbleManager.Instance.RumblePulse(0.75f, 0.75f, 0.25f);
            }

            CameraShake.Instance.ExplosionShake();

            SetState(State.GameOver);
            return;
        }

        float softLandingVelocityMagnitude = 4f;
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;

        if (relativeVelocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Landed too fast!");
            OnLanded?.Invoke(this, new LandedEventArgs
            {
                landingType = LandingType.TooFastLanding,
                dotVector = 0f,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = 0f,
                score = 0,
            });

            if (GameInputs.Instance.IsGamePadConnected())
            {
                RumbleManager.Instance.RumblePulse(0.75f, 0.75f, 0.25f);
            }

            CameraShake.Instance.ExplosionShake();

            SetState(State.GameOver);
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float softAngle = .90f;

        if (dotVector < softAngle)
        {
            Debug.Log("Landed on a bad angle");
            OnLanded?.Invoke(this, new LandedEventArgs
            {
                landingType = LandingType.TooSteepAngle,
                dotVector = dotVector,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = 0f,
                score = 0,
            });

            if (GameInputs.Instance.IsGamePadConnected())
            {
                RumbleManager.Instance.RumblePulse(0.75f, 0.75f, 0.25f);
            }

            CameraShake.Instance.ExplosionShake();

            SetState(State.GameOver);
            return;
        }

        Debug.Log("Soft Landing!");

        float maxScoreAmountAngleLanding = 100f;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountAngleLanding - Mathf.Abs(dotVector - 1) * scoreDotVectorMultiplier * maxScoreAmountAngleLanding;

        float maxScoreAmountLandingSpeed = 100f;
        float speedLandingScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;

        int score = Mathf.RoundToInt((landingAngleScore + speedLandingScore) * landingPad.GetScoreMultiplier());

        Debug.Log("Score: " + score);

        OnLanded?.Invoke(this, new LandedEventArgs
        {
            landingType = LandingType.Success,
            dotVector = dotVector,
            landingSpeed = relativeVelocityMagnitude,
            scoreMultiplier = landingPad.GetScoreMultiplier(),
            score = score,
        });

        if (GameInputs.Instance.IsGamePadConnected())
        {
            RumbleManager.Instance.RumblePulse(0.01f, 0.01f, 0.5f);
        }

        SetState(State.GameOver);
    }


    public void GrabFuelPickUp(FuelPickUp fuelPickUp)
    {
        float addFuelAmount = 10f;
        fuelAmount += addFuelAmount;
        fuelAmount = Mathf.Min(fuelAmount, fuelAmountMax);

        OnFuelPickUp?.Invoke(this, EventArgs.Empty);

        CameraShake.Instance.Shake(5, 3, 0.25f);

        fuelPickUp.DestroySelf();
    }

    public void GrabCoinPickUp(CoinPickUp coinPickUp)
    {
        OnCoinPickUp?.Invoke(this, EventArgs.Empty);
        CameraShake.Instance.Shake(5, 3, 0.25f);
        coinPickUp.DestroySelf();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out IInteractableStay interactable))
        {
            interactable.Stay(this);
        }
    }

    private void SetState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, new StateChangedEventArgs { state = state });
    }

    private void ConsumeFuel()
    {
        float fuelConsumptionRate = 1f;
        fuelAmount -= fuelConsumptionRate * Time.deltaTime;
        CheckLowFuel();
    }

    private void CheckLowFuel()
    {
        if (!hasTriggeredLowFuel && GetFuelNormalized() < fuelLowAmount)
        {
            hasTriggeredLowFuel = true;
            OnLowFuel?.Invoke(this, EventArgs.Empty);
        }

        if (hasTriggeredLowFuel && GetFuelNormalized() >= fuelLowAmount)
        {
            hasTriggeredLowFuel = false;
            OnHighFuel?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetFuel() => fuelAmount;
    public float GetFuelNormalized() => fuelAmount / fuelAmountMax;
    public float GetSpeedX() => landerRigidBody2D.linearVelocityX * 3f;
    public float GetSpeedY() => landerRigidBody2D.linearVelocityY * 3f;
}
