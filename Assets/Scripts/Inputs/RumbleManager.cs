using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager Instance { get; private set; }

    private Gamepad pad;

    private void Awake()
    {
        Instance = this;
    }

    
    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            // Set the motor speeds for rumble
            pad.SetMotorSpeeds(lowFrequency, highFrequency);

            // Stop the rumble after the specified duration
            StartCoroutine(StopRumble(duration, pad));
        }   
    }

    // Coroutine to stop rumble after a delay
    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0f,0f);  
    }
}
