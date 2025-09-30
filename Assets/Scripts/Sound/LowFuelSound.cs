using UnityEngine;

public class LowFuelSound : MonoBehaviour
{
    [SerializeField] private AudioSource lowFuelAudioSource;

    private bool isLowFuel;

    private void Start()
    {
        lowFuelAudioSource.Pause();

        SoundManager.Instance.OnSoundVolumeChanged += SoundManager_OnSoundVolumeChanged;

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;

        Lander.Instance.OnLowFuel += Lander_OnLowFuel;
        Lander.Instance.OnHighFuel += Lander_OnHighFuel;
    }

    private void GameManager_OnGameResumed(object sender, System.EventArgs e)
    {
        if (isLowFuel)
        {
            lowFuelAudioSource.Play();
        }
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        lowFuelAudioSource.Pause();
    }

    private void Lander_OnHighFuel(object sender, System.EventArgs e)
    {
        lowFuelAudioSource.Pause();
    }

    private void Lander_OnLowFuel(object sender, System.EventArgs e)
    {
        lowFuelAudioSource.Play();
        isLowFuel = true;
    }

    private void SoundManager_OnSoundVolumeChanged(object sender, System.EventArgs e)
    {
        lowFuelAudioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed -= GameManager_OnGameResumed;
    }
}
