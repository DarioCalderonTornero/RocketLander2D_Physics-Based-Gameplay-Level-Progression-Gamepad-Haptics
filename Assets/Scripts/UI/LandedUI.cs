using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI restartButtonText;
    [SerializeField] private Button restartButton;

    private Action nextButtonClickAction;
    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            nextButtonClickAction();
        }); 

    }

    private void Start()
    {
        Lander.Instance.OnLanded += Instance_OnLanded;
        Hide();
    }

    private void Instance_OnLanded(object sender, Lander.LandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {           
            titleText.text = "<color=#00ff00>SUCCESFUL LANDING!</color>";
            restartButtonText.text = "CONTINUE";    
            nextButtonClickAction = GameManager.Instance.GoToNextLevel;
        }
        else
        {
            titleText.text = "<color=#ff0000>CRASH!</color>";
            restartButtonText.text = "RETRY";
            nextButtonClickAction = GameManager.Instance.RestartLevel;
        }

        statsText.text =
        statsText.text =
        Mathf.Round(e.landingSpeed * 2f) + "\n" +
        Mathf.Round(e.dotVector * 100f) + "\n" +
        "X " + "" + e.scoreMultiplier + "\n" +
        e.score;

        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        restartButton.Select(); 
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
