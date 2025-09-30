using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public event EventHandler OnSoundVolumeChanged;

    private const int SOUND_VOLUME_MAX = 10;
    private static int soundVolume = 6;


    [SerializeField] private AudioClip coinPickUpSound;
    [SerializeField] private AudioClip fuelPickUpSound;
    [SerializeField] private AudioClip landingSuccessSound;
    [SerializeField] private AudioClip landingCrashSound;
    [SerializeField] private AudioClip lowFuelSound;

    public List<AudioClip> musicSounds = new List<AudioClip>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {


        Lander.Instance.OnCoinPickUp += Lander_OnCoinPickUp;
        Lander.Instance.OnFuelPickUp += Lander_OnFuelPickUp;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.LandedEventArgs e)
    {
        switch(e.landingType)
        {
            case Lander.LandingType.Success:
                AudioSource.PlayClipAtPoint(landingSuccessSound, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
            default:
                AudioSource.PlayClipAtPoint(landingCrashSound, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
        }
    }

    private void Lander_OnFuelPickUp(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(fuelPickUpSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    private void Lander_OnCoinPickUp(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickUpSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    public void ChangeSoundVolume()
    {
        soundVolume = (soundVolume + 1) % SOUND_VOLUME_MAX;
        OnSoundVolumeChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSoundVolume()
    {
        return soundVolume;
    }

    public float GetSoundVolumeNormalized()
    {
        return ((float)soundVolume) / SOUND_VOLUME_MAX;
    }

}

    
