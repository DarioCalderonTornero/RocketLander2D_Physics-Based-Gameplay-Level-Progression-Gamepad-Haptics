using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance { get; private set; }

    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin perlin;

    private float shakeTimer;
    private float shakeDuration;
    private float startingAmplitude;

    private void Awake()
    {
        Instance = this;

        perlin.AmplitudeGain = 0f;
    }

    public void Shake(float amplitude, float frequency, float duration)
    {
        perlin.AmplitudeGain = amplitude;
        perlin.FrequencyGain = frequency;

        startingAmplitude = amplitude;
        shakeDuration = duration;
        shakeTimer = duration;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            float normalized = 1 - (shakeTimer / shakeDuration);
            perlin.AmplitudeGain = Mathf.Lerp(startingAmplitude, 0f, normalized);
        }
    }

    public void ExplosionShake() => Shake(25f, 30f, 1f);
}
