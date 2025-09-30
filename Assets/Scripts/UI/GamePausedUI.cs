using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    //Main Buttons
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    //Settings Buttons/Texts
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button soundVolumeButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private Button backButton;

    private bool settingsOpen = false;
    private bool gamePaused = false;


    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGamePause();
        });

        settingsButton.onClick.AddListener(() =>
        {
            ShowSettings();
            HideMainButtons();
        });

        soundVolumeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundText.text = "SOUND VOLUME: " + SoundManager.Instance.GetSoundVolume();
        }); 

        musicVolumeButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeSoundVolume();
            musicText.text = "MUSIC VOLUME: " + MusicManager.Instance.GetSoundVolume();
        }); 

        backButton.onClick.AddListener(() =>
        {
            ShowMainButtons();
            HideSettings();
        }); 

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        GameInputs.Instance.OnBackPauseGame += GameInputs_OnBackPauseGame;

        soundText.text = "SOUND VOLUME: " + SoundManager.Instance.GetSoundVolume();
        musicText.text = "MUSIC VOLUME: " + MusicManager.Instance.GetSoundVolume();

        Hide();
    }

    private void GameInputs_OnBackPauseGame(object sender, System.EventArgs e)
    {
        if (settingsOpen)
        {
            HideSettings();
            ShowMainButtons();
        }

        else if (!settingsOpen && gamePaused)
        {
            GameManager.Instance.ToggleGamePause();
        }
    }

    private void GameManager_OnGameResumed(object sender, System.EventArgs e)
    {
        Hide();
        gamePaused = false;
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
        HideSettings();
        gamePaused = true;
    }

    private void Show()
    {
        gameObject.SetActive(true);
        ShowMainButtons();
        if (GameInputs.Instance.IsGamePadConnected())
        {
            resumeButton.Select();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowSettings()
    {
        if (GameInputs.Instance. IsGamePadConnected())
        {
            soundVolumeButton.Select();
        }

        musicVolumeButton.gameObject.SetActive(true);   
        soundVolumeButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        settingsOpen = true;
    }

    private void HideSettings()
    {
        if (GameInputs.Instance.IsGamePadConnected())
        {
            resumeButton.Select();
        }
        musicVolumeButton.gameObject.SetActive(false);   
        soundVolumeButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        settingsOpen = false;
    }

    private void ShowMainButtons()
    {
        resumeButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    private void HideMainButtons()
    {
        resumeButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }
}
