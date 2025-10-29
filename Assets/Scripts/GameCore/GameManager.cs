using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;

    private int score;
    private float time;

    private bool isTimerActive;

    private static int levelNumber = 1;
    private static int totalScore;
    private static int addScoreAmount = 500;

    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private bool tutorialPlayed = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Lander_OnCoinPickUp;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;

        GameInputs.Instance.OnPauseGame += GameInputs_OnPauseGame;

        LoadCurrentLevel();
    }

    public static void ResetStaticData()
    {
        levelNumber = 1;
        totalScore = 0; 
    }

    private void GameInputs_OnPauseGame(object sender, System.EventArgs e)
    {
        ToggleGamePause();
    }

    public void ToggleGamePause()
    {
        if (Time.timeScale == 1f)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        OnGameResumed?.Invoke(this, EventArgs.Empty);
    }

    private void Lander_OnStateChanged(object sender, Lander.StateChangedEventArgs e)
    {
        isTimerActive = e.state == Lander.State.Playing;

        if (e.state == Lander.State.Playing)
        {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetNormalOrthographicSize();
        }
    }

    private void Update()
    {
        if (!isTimerActive) return;
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
            GoToNextLevel();
    }
    private void LoadCurrentLevel()
    {
        GameLevel gameLevel = GetGameLevel();
        GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);  
        Lander.Instance.transform.position = spawnedGameLevel.GetStartLanderPosition();
        cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.GetCameraStartTargetTransform();
        CinemachineCameraZoom2D.Instance.SetTargetOrthographicSize(spawnedGameLevel.GetZoomOutOrtographicSize());   
    }

    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gamelevel in gameLevelList)
        {
            if (gamelevel.GetLevelNumber() == levelNumber)
            {
                return gamelevel;
            }
        }

        return null;
    }
    private void Lander_OnLanded(object sender, Lander.LandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickUp(object sender, System.EventArgs e)
    {
        AddScore(addScoreAmount);
        //CameraShake.Instance.Shake(5, 3, 0.25f);
    }

    public void AddScore(int addScoreAmount)
    {
        score += addScoreAmount;
        Debug.Log(score);
    }

    private void OnDestroy()
    {
        Lander.Instance.OnCoinPickUp -= Lander_OnCoinPickUp;
    }

    public int GetScore()
    {
        return score;
    }

    public float GetTime()
    {
        return time;
    }

    public static int GetTotalScore()
    {
        return totalScore;
    }   

    public void GoToNextLevel()
    {
        levelNumber++;
        totalScore += score;

        //gameLevelList.RemoveAll(level =>  level.GetLevelNumber() == 1);

        if(GetGameLevel() == null)
        {
            // No more levels, go to finish screen
            SceneLoader.LoadScene(SceneLoader.Scene.GameFinishScene);
            return;
        }
        else
        {
            // Load next level
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        }   
    }

    public void RestartLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }
}
